import { DestroyRef, computed, inject, signal } from "@angular/core";
import { FormBuilder } from "@angular/forms";
import { ConfirmationService } from "primeng/api";
import { ToastService } from "@core/services/toast.service";
import { GastroDataService } from "@core/backend-api/services/gastro-data.service";
import { setApiErrors } from "@core";
import { applyGastroFilters, formatGastroCell, GastroFilters } from "./gastro-table.helpers";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";
import { startWith, Observable } from "rxjs";

export abstract class BuletineBaseComponent<
  T extends { id?: number; nr?: number | string; nume?: string; prenume?: string; cnp?: string }
> {
  protected readonly gastroService = inject(GastroDataService);
  protected readonly confirmationService = inject(ConfirmationService);
  protected readonly toast = inject(ToastService);
  protected readonly fb = inject(FormBuilder);
  protected readonly destroyRef = inject(DestroyRef);

  readonly records = signal<Array<T>>([]);
  readonly loading = signal(true);
  readonly errors = signal(new Array<string>());
  readonly filters = signal<GastroFilters>({ nr: "", nume: "", prenume: "", cnp: "" });
  readonly applyApiErrors = setApiErrors(this.errors);

  readonly form = this.fb.group({
    nr: this.fb.control(""),
    nume: this.fb.control(""),
    prenume: this.fb.control(""),
    cnp: this.fb.control(""),
  });

  readonly filteredRecords = computed(() => applyGastroFilters(this.records(), this.filters()));
  readonly tableScrollHeight = "calc(90vh - 280px)";

  constructor() {
    this.form.valueChanges.pipe(startWith(this.form.value), takeUntilDestroyed()).subscribe(value => {
      this.filters.set({
        nr: value.nr ?? "",
        nume: value.nume ?? "",
        prenume: value.prenume ?? "",
        cnp: value.cnp ?? "",
      });
    });

    this.loadRecords();
  }

  formatCell(field: string, value: unknown) {
    return formatGastroCell(field, value);
  }

  deleteRecord(event: Event, recordId: number) {
    const target = event.target ?? undefined;
    this.confirmationService.confirm({
      header: "Confirmare ETtergere",
      message: "SunteE>i sigur cŽŸ doriE>i sŽŸ ETtergeE>i acest buletin?",
      key: this.getDeleteConfirmKey(recordId),
      target,
      accept: () => {
        this.deleteRecordRequest(recordId)
          .pipe(takeUntilDestroyed(this.destroyRef))
          .subscribe({
            next: () => {
              this.records.set(this.records().filter(item => item.id !== recordId));
              this.errors.set([]);
              this.toast.success("Buletin ETters cu succes.");
            },
            error: response => {
              this.applyApiErrors(response);
            },
          });
      },
    });
  }

  protected loadRecords() {
    this.loading.set(true);
    this.getRecordsRequest()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: data => {
          const sorted = [...data].sort((a, b) => Number(b.nr ?? 0) - Number(a.nr ?? 0));
          this.records.set(sorted);
          this.errors.set([]);
          this.loading.set(false);
        },
        error: response => {
          this.applyApiErrors(response);
          this.loading.set(false);
        },
      });
  }

  protected abstract getRecordsRequest(): Observable<Array<T>>;
  protected abstract deleteRecordRequest(recordId: number): Observable<boolean>;
  protected abstract getDeleteConfirmKey(recordId: number): string;

  protected fmtDate(v: unknown): string {
    if (!v) return '';
    const m = String(v).match(/^(\d{4})-(\d{2})-(\d{2})/);
    return m ? `${m[1]}-${m[2]}-${m[3]}` : String(v);
  }

  protected fmtTime(v: unknown): string {
    if (!v) return '';
    const m = String(v).match(/T(\d{2}:\d{2})/);
    return m ? m[1] : String(v);
  }

  protected doPrint(html: string) {
    const div = document.createElement('div');
    div.className = 'bp-page';
    div.innerHTML = html;
    document.body.appendChild(div);
    document.body.dataset['printing'] = 'true';

    const trigger = () => {
      window.print();
      delete document.body.dataset['printing'];
      document.body.removeChild(div);
    };

    const img = div.querySelector<HTMLImageElement>('img');
    if (img && !img.complete) {
      img.onload = trigger;
      img.onerror = trigger;
    } else {
      trigger();
    }
  }
}
