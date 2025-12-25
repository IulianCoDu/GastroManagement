import { Component, DestroyRef, computed, inject, signal } from "@angular/core";
import { FormBuilder, ReactiveFormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { RouterLink } from "@angular/router";
import { PanelModule } from "primeng/panel";
import { ProgressSpinnerModule } from "primeng/progressspinner";
import { TableModule } from "primeng/table";
import { InputTextModule } from "primeng/inputtext";
import { ButtonModule } from "primeng/button";
import { ConfirmationService } from "primeng/api";
import { GastroTableColumn } from "@core/features/gastro/components/gastro-table/gastro-table.component";
import { BuletinEdiDto } from "@core/backend-api/dtos";
import { GastroDataService } from "@core/backend-api/services/gastro-data.service";
import { setApiErrors } from "@core";
import { ErrorListComponent } from "@core/components/error-list/error-list.component";
import { ConfirmPopupComponent } from "@core/components/confirm-popup/confirm-popup.component";
import { ToastService } from "@core/services/toast.service";
import { UserSettingKeys } from "@core/backend-api";
import { GastroChartConfig, GastroChartsComponent } from "@core/features/gastro/components/gastro-charts/gastro-charts.component";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";
import { startWith } from "rxjs";
import { applyGastroFilters, GastroFilters } from "../gastro-table.helpers";

@Component({
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    PanelModule,
    TableModule,
    InputTextModule,
    ButtonModule,
    ProgressSpinnerModule,
    ConfirmPopupComponent,
    ErrorListComponent,
    GastroChartsComponent,
    RouterLink,
  ],
  templateUrl: "./buletine-edi.component.html",
  styleUrls: ["../gastro-table.page.css"],
})
export class BuletineEdiComponent {
  readonly #gastroService = inject(GastroDataService);
  readonly #confirmationService = inject(ConfirmationService);
  readonly #toast = inject(ToastService);
  readonly #fb = inject(FormBuilder);
  readonly #destroyRef = inject(DestroyRef);

  readonly records = signal<Array<BuletinEdiDto>>([]);
  readonly loading = signal(true);
  readonly errors = signal(new Array<string>());
  readonly #applyApiErrors = setApiErrors(this.errors);
  readonly filters = signal<GastroFilters>({ nr: "", nume: "", prenume: "", cnp: "" });

  readonly form = this.#fb.group({
    nr: this.#fb.control(""),
    nume: this.#fb.control(""),
    prenume: this.#fb.control(""),
    cnp: this.#fb.control(""),
  });

  readonly filteredRecords = computed(() => applyGastroFilters(this.records(), this.filters()));
  readonly tableScrollHeight = "calc(90vh - 280px)";
  readonly chartSettingsKey = UserSettingKeys.GastroChartsEdi;
  readonly defaultCharts: Array<GastroChartConfig> = [
    { id: "age-range", title: "Distribuitie varsta pacienti", field: "virsta", mode: "age-range", maxItems: 0 },
  ];

  readonly columns: Array<GastroTableColumn> = [
    { field: "nr", header: "Nr. reg." },
    { field: "nume", header: "Nume" },
    { field: "prenume", header: "Prenume" },
    { field: "virsta", header: "Vârstă" },
    { field: "cnp", header: "CNP" },
    { field: "domiciliu", header: "Domiciliu" },
    { field: "diagnostic", header: "Diagnostic" },
    { field: "sistem", header: "Sistem" },
    { field: "sedareT", header: "Sedare" },
    { field: "medicatie", header: "Medicație" },
    { field: "ileon", header: "Ileon" },
    { field: "cec", header: "Cec" },
    { field: "ascendent", header: "Ascendent" },
    { field: "transvers", header: "Transvers" },
    { field: "descendent", header: "Descendent" },
    { field: "sigmoid", header: "Sigmoid" },
    { field: "rect", header: "Rect" },
    { field: "jar", header: "JAR" },
    { field: "biopsiiL", header: "Biopsii L" },
    { field: "biopsiiN", header: "Biopsii N" },
    { field: "nrap", header: "Nr AP" },
    { field: "biopsiiR", header: "Rezultate" },
    { field: "tratament", header: "Tratament" },
    { field: "data", header: "Data" },
    { field: "ora", header: "Ora" },
    { field: "medic", header: "Medic" },
    { field: "biopsiiL1", header: "Biopsii L1" },
    { field: "biopsiiN1", header: "Biopsii N1" },
    { field: "nrap1", header: "Nr AP 1" },
    { field: "biopsiiR1", header: "Rezultate 1" },
    { field: "data1", header: "Data 1" },
    { field: "ora1", header: "Ora 1" },
    { field: "sedareT1", header: "Sedare 1" },
    { field: "dozaS", header: "Doza S" },
    { field: "medicatie1", header: "Medicație 1" },
    { field: "dozaM", header: "Doza M" },
    { field: "hasFig1", header: "Fig1" },
    { field: "hasFig2", header: "Fig2" },
    { field: "hasFig3", header: "Fig3" },
    { field: "hasFilm1", header: "Film" },
    { field: "consumabile", header: "Consumabile" },
    { field: "materiale", header: "Materiale" },
  ];

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

  deleteRecord(event: Event, recordId: number) {
    const target = event.target ?? undefined;
    this.#confirmationService.confirm({
      header: "Confirmare ștergere",
      message: "Sunteți sigur că doriți să ștergeți acest buletin?",
      key: `buletin-edi-${recordId}`,
      target,
      accept: () => {
        this.#gastroService
          .deleteBuletineEdi(recordId)
          .pipe(takeUntilDestroyed(this.#destroyRef))
          .subscribe({
            next: () => {
              this.records.set(this.records().filter(item => item.id !== recordId));
              this.errors.set([]);
              this.#toast.success("Buletin șters cu succes.");
            },
            error: response => {
              this.#applyApiErrors(response);
            },
          });
      },
    });
  }

  private loadRecords() {
    this.loading.set(true);
    this.#gastroService
      .getBuletineEdi()
      .pipe(takeUntilDestroyed())
      .subscribe({
        next: data => {
          const sorted = [...data].sort((a, b) => (b.nr ?? 0) - (a.nr ?? 0));
          this.records.set(sorted);
          this.errors.set([]);
          this.loading.set(false);
        },
        error: response => {
          this.#applyApiErrors(response);
          this.loading.set(false);
        },
      });
  }
}
