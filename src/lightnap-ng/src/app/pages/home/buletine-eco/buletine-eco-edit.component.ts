import { CommonModule } from "@angular/common";
import { Component, DestroyRef, OnInit, computed, inject, input, signal } from "@angular/core";
import { FormBuilder, ReactiveFormsModule } from "@angular/forms";
import { RouterLink } from "@angular/router";
import { CreateBuletinEcoDto, MedicLookupDto, UpdateBuletinEcoDto, setApiErrors } from "@core";
import { ErrorListComponent } from "@core/components/error-list/error-list.component";
import { GastroDataService } from "@core/backend-api/services/gastro-data.service";
import { RouteAliasService } from "@core/features/routing/services/route-alias-service";
import { ToastService } from "@core/services/toast.service";
import { ButtonModule } from "primeng/button";
import { InputTextModule } from "primeng/inputtext";
import { PanelModule } from "primeng/panel";
import { ProgressSpinnerModule } from "primeng/progressspinner";
import { SelectModule } from "primeng/select";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";
import { forkJoin } from "rxjs";
import { BuletineEditBaseComponent } from "../buletine-edit-base.component";

@Component({
  standalone: true,
  templateUrl: "./buletine-eco-edit.component.html",
  imports: [
    CommonModule,
    ReactiveFormsModule,
    PanelModule,
    InputTextModule,
    SelectModule,
    ButtonModule,
    ProgressSpinnerModule,
    ErrorListComponent,
    RouterLink,
  ],
})
export class BuletineEcoEditComponent extends BuletineEditBaseComponent implements OnInit {
  readonly #gastroService = inject(GastroDataService);
  readonly #routeAlias = inject(RouteAliasService);
  readonly #toast = inject(ToastService);
  readonly #fb = inject(FormBuilder);
  readonly #destroyRef = inject(DestroyRef);

  readonly id = input<string | undefined>();
  readonly recordId = computed(() => {
    const value = this.id();
    if (!value) return undefined;
    const parsed = Number(value);
    return Number.isNaN(parsed) ? undefined : parsed;
  });

  readonly loading = signal(false);
  readonly errors = signal(new Array<string>());
  readonly medici = signal<Array<MedicLookupDto>>([]);
  readonly #applyApiErrors = setApiErrors(this.errors);

  readonly form = this.#fb.group({
    nr: this.#fb.control({ value: "", disabled: true }),
    nume: this.#fb.control(""),
    prenume: this.#fb.control(""),
    virsta: this.#fb.control<number | null>(null),
    cnp: this.#fb.control(""),
    seriaCs: this.#fb.control(""),
    domiciliu: this.#fb.control(""),
    telefon: this.#fb.control(""),
    diagnostic: this.#fb.control(""),
    ficat: this.#fb.control(""),
    colecist: this.#fb.control(""),
    vp: this.#fb.control<number | null>(null),
    vs: this.#fb.control<number | null>(null),
    cbp: this.#fb.control<number | null>(null),
    pancreas: this.#fb.control(""),
    splina: this.#fb.control(""),
    rd: this.#fb.control(""),
    rs: this.#fb.control(""),
    vu: this.#fb.control(""),
    prostata: this.#fb.control(""),
    ogi: this.#fb.control(""),
    obs: this.#fb.control(""),
    data: this.#fb.control(""),
    ora: this.#fb.control(""),
    medic: this.#fb.control(""),
    medicId: this.#fb.control<number | null>(null),
    ceus: this.#fb.control(""),
  });

  ngOnInit() {
    const id = this.recordId();
    if (!id) {
      this.loadMedici();
      return;
    }

    this.loading.set(true);
    forkJoin({
      medici: this.#gastroService.getMediciLookup(),
      record: this.#gastroService.getBuletinEco(id),
    })
      .pipe(takeUntilDestroyed(this.#destroyRef))
      .subscribe({
        next: ({ medici, record }) => {
          this.medici.set(medici);
          if (record) {
            const resolvedMedicId = record.medicId ?? this.resolveMedicId(record.medic, medici);
            this.form.patchValue({
              nr: record.nr?.toString() ?? "",
              nume: record.nume ?? "",
              prenume: record.prenume ?? "",
              virsta: record.virsta ?? null,
              cnp: record.cnp ?? "",
              seriaCs: record.seriaCs ?? "",
              domiciliu: record.domiciliu ?? "",
              telefon: record.telefon ?? "",
              diagnostic: record.diagnostic ?? "",
              ficat: record.ficat ?? "",
              colecist: record.colecist ?? "",
              vp: record.vp ?? null,
              vs: record.vs ?? null,
              cbp: record.cbp ?? null,
              pancreas: record.pancreas ?? "",
              splina: record.splina ?? "",
              rd: record.rd ?? "",
              rs: record.rs ?? "",
              vu: record.vu ?? "",
              prostata: record.prostata ?? "",
              ogi: record.ogi ?? "",
              obs: record.obs ?? "",
              data: this.formatDate(record.data),
              ora: this.formatTime(record.ora),
              medic: record.medic ?? "",
              medicId: resolvedMedicId,
              ceus: record.ceus ?? "",
            });
          }
          this.loading.set(false);
        },
        error: response => {
          this.#applyApiErrors(response);
          this.loading.set(false);
        },
      });
  }

  onSave() {
    this.errors.set([]);
    const payload = this.buildPayload();
    const id = this.recordId();

    if (id) {
      this.#gastroService.updateBuletinEco(id, payload).subscribe({
        next: () => {
          this.#toast.success("Buletin ECO a fost actualizat.");
          this.#routeAlias.navigate("buletine-eco");
        },
        error: this.#applyApiErrors,
      });
      return;
    }

    this.#gastroService.createBuletinEco(payload).subscribe({
      next: created => {
        this.#toast.success("Buletin ECO a fost creat.");
        this.form.reset();
        this.#routeAlias.navigate("buletine-eco");
      },
      error: this.#applyApiErrors,
    });
  }

  private buildPayload(): CreateBuletinEcoDto & UpdateBuletinEcoDto {
    const value = this.form.getRawValue();
    const resolvedMedic = this.lookupMedicName(value.medicId) ?? value.medic ?? undefined;
    return {
      nume: value.nume ?? "",
      prenume: value.prenume ?? "",
      virsta: value.virsta ?? 0,
      cnp: value.cnp ?? "",
      seriaCs: value.seriaCs || undefined,
      domiciliu: value.domiciliu || undefined,
      telefon: value.telefon || undefined,
      diagnostic: value.diagnostic || undefined,
      ficat: value.ficat || undefined,
      colecist: value.colecist || undefined,
      vp: value.vp ?? undefined,
      vs: value.vs ?? undefined,
      cbp: value.cbp ?? undefined,
      pancreas: value.pancreas || undefined,
      splina: value.splina || undefined,
      rd: value.rd || undefined,
      rs: value.rs || undefined,
      vu: value.vu || undefined,
      prostata: value.prostata || undefined,
      ogi: value.ogi || undefined,
      obs: value.obs || undefined,
      data: this.toIsoDate(value.data),
      ora: this.toIsoDateTime(value.data, value.ora),
      medicId: value.medicId ?? undefined,
      medic: resolvedMedic ?? "",
      ceus: value.ceus || undefined,
    };
  }

  private loadMedici() {
    this.#gastroService
      .getMediciLookup()
      .pipe(takeUntilDestroyed(this.#destroyRef))
      .subscribe({
        next: medici => this.medici.set(medici),
        error: this.#applyApiErrors,
      });
  }

  private resolveMedicId(medicName: string | undefined, medici: Array<MedicLookupDto>) {
    if (!medicName) return null;
    const match = medici.find(medic => medic.medicName.toLowerCase() === medicName.trim().toLowerCase());
    return match?.id ?? null;
  }

  private lookupMedicName(medicId: number | null | undefined) {
    if (medicId == null) return undefined;
    return this.medici().find(medic => medic.id === medicId)?.medicName;
  }

}
