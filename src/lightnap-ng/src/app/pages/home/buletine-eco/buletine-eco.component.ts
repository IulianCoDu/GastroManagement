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
import { BuletinEcoDto } from "@core/backend-api/dtos";
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
  templateUrl: "./buletine-eco.component.html",
  styleUrls: ["../gastro-table.page.css"],
})
export class BuletineEcoComponent {
  readonly #gastroService = inject(GastroDataService);
  readonly #confirmationService = inject(ConfirmationService);
  readonly #toast = inject(ToastService);
  readonly #fb = inject(FormBuilder);
  readonly #destroyRef = inject(DestroyRef);

  readonly records = signal<Array<BuletinEcoDto>>([]);
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
  readonly chartSettingsKey = UserSettingKeys.GastroChartsEco;
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
    { field: "telefon", header: "Telefon" },
    { field: "diagnostic", header: "Diagnostic" },
    { field: "ficat", header: "Ficat", width: "320px" },
    { field: "colecist", header: "Colecist" },
    { field: "vp", header: "VP" },
    { field: "vs", header: "VS" },
    { field: "cbp", header: "CBP" },
    { field: "pancreas", header: "Pancreas" },
    { field: "splina", header: "Splină" },
    { field: "rd", header: "RD" },
    { field: "rs", header: "RS" },
    { field: "vu", header: "VU" },
    { field: "prostata", header: "Prostată" },
    { field: "ogi", header: "OGI" },
    { field: "obs", header: "Observații" },
    { field: "data", header: "Data" },
    { field: "ora", header: "Ora" },
    { field: "medic", header: "Medic" },
    { field: "ceus", header: "CEUS" },
    { field: "hasFig1", header: "Fig1" },
    { field: "hasFig2", header: "Fig2" },
    { field: "hasFig3", header: "Fig3" },
    { field: "hasFilm1", header: "Film" },
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
      key: `buletin-eco-${recordId}`,
      target,
      accept: () => {
        this.#gastroService
          .deleteBuletineEco(recordId)
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
      .getBuletineEco()
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
