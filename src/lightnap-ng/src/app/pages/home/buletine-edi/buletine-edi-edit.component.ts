import { CommonModule } from "@angular/common";
import { Component, DestroyRef, OnInit, computed, inject, input, signal } from "@angular/core";
import { FormBuilder, ReactiveFormsModule } from "@angular/forms";
import { RouterLink } from "@angular/router";
import { CreateBuletinEdiDto, MedicLookupDto, UpdateBuletinEdiDto, setApiErrors } from "@core";
import { ErrorListComponent } from "@core/components/error-list/error-list.component";
import { GastroDataService } from "@core/backend-api/services/gastro-data.service";
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
  templateUrl: "./buletine-edi-edit.component.html",
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
export class BuletineEdiEditComponent extends BuletineEditBaseComponent implements OnInit {
  readonly #gastroService = inject(GastroDataService);
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
  get printDate() { return new Date().toLocaleDateString('ro-RO', { day: '2-digit', month: '2-digit', year: 'numeric' }); }

  readonly form = this.#fb.group({
    nr: this.#fb.control({ value: "", disabled: true }),
    nume: this.#fb.control(""),
    prenume: this.#fb.control(""),
    virsta: this.#fb.control<number | null>(null),
    cnp: this.#fb.control(""),
    domiciliu: this.#fb.control(""),
    diagnostic: this.#fb.control(""),
    sistem: this.#fb.control(""),
    sedareT: this.#fb.control(""),
    medicatie: this.#fb.control(""),
    ileon: this.#fb.control(""),
    cec: this.#fb.control(""),
    ascendent: this.#fb.control(""),
    transvers: this.#fb.control(""),
    descendent: this.#fb.control(""),
    sigmoid: this.#fb.control(""),
    rect: this.#fb.control(""),
    jar: this.#fb.control(""),
    biopsiiL: this.#fb.control(""),
    biopsiiN: this.#fb.control<number | null>(null),
    nrap: this.#fb.control<number | null>(null),
    biopsiiR: this.#fb.control(""),
    tratament: this.#fb.control(""),
    data: this.#fb.control(""),
    ora: this.#fb.control(""),
    medic: this.#fb.control(""),
    medicId: this.#fb.control<number | null>(null),
    biopsiiL1: this.#fb.control(""),
    biopsiiN1: this.#fb.control<number | null>(null),
    nrap1: this.#fb.control<number | null>(null),
    biopsiiR1: this.#fb.control(""),
    data1: this.#fb.control(""),
    ora1: this.#fb.control(""),
    sedareT1: this.#fb.control(""),
    dozaS: this.#fb.control<number | null>(null),
    medicatie1: this.#fb.control(""),
    dozaM: this.#fb.control<number | null>(null),
    consumabile: this.#fb.control(""),
    materiale: this.#fb.control(""),
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
      record: this.#gastroService.getBuletinEdi(id),
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
              domiciliu: record.domiciliu ?? "",
              diagnostic: record.diagnostic ?? "",
              sistem: record.sistem ?? "",
              sedareT: record.sedareT ?? "",
              medicatie: record.medicatie ?? "",
              ileon: record.ileon ?? "",
              cec: record.cec ?? "",
              ascendent: record.ascendent ?? "",
              transvers: record.transvers ?? "",
              descendent: record.descendent ?? "",
              sigmoid: record.sigmoid ?? "",
              rect: record.rect ?? "",
              jar: record.jar ?? "",
              biopsiiL: record.biopsiiL ?? "",
              biopsiiN: record.biopsiiN ?? null,
              nrap: record.nrap ?? null,
              biopsiiR: record.biopsiiR ?? "",
              tratament: record.tratament ?? "",
              data: this.formatDate(record.data),
              ora: this.formatTime(record.ora),
              medic: record.medic ?? "",
              medicId: resolvedMedicId,
              biopsiiL1: record.biopsiiL1 ?? "",
              biopsiiN1: record.biopsiiN1 ?? null,
              nrap1: record.nrap1 ?? null,
              biopsiiR1: record.biopsiiR1 ?? "",
              data1: this.formatDate(record.data1),
              ora1: this.formatTime(record.ora1),
              sedareT1: record.sedareT1 ?? "",
              dozaS: record.dozaS ?? null,
              medicatie1: record.medicatie1 ?? "",
              dozaM: record.dozaM ?? null,
              consumabile: record.consumabile ?? "",
              materiale: record.materiale ?? "",
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
      this.#gastroService.updateBuletinEdi(id, payload).subscribe({
        next: () => {
          this.#toast.success("Buletin EDI a fost actualizat.");
        },
        error: this.#applyApiErrors,
      });
      return;
    }

    this.#gastroService.createBuletinEdi(payload).subscribe({
      next: () => {
        this.#toast.success("Buletin EDI a fost creat.");
        this.form.reset();
      },
      error: this.#applyApiErrors,
    });
  }

  private buildPayload(): CreateBuletinEdiDto & UpdateBuletinEdiDto {
    const value = this.form.getRawValue();
    const resolvedMedic = this.lookupMedicName(value.medicId) ?? value.medic ?? undefined;
    return {
      nume: value.nume || undefined,
      prenume: value.prenume || undefined,
      virsta: value.virsta ?? undefined,
      cnp: value.cnp || undefined,
      domiciliu: value.domiciliu || undefined,
      diagnostic: value.diagnostic || undefined,
      sistem: value.sistem ?? "",
      sedareT: value.sedareT || undefined,
      medicatie: value.medicatie || undefined,
      ileon: value.ileon || undefined,
      cec: value.cec || undefined,
      ascendent: value.ascendent || undefined,
      transvers: value.transvers || undefined,
      descendent: value.descendent || undefined,
      sigmoid: value.sigmoid || undefined,
      rect: value.rect || undefined,
      jar: value.jar || undefined,
      biopsiiL: value.biopsiiL || undefined,
      biopsiiN: value.biopsiiN ?? undefined,
      nrap: value.nrap ?? undefined,
      biopsiiR: value.biopsiiR || undefined,
      tratament: value.tratament || undefined,
      data: this.toIsoDate(value.data),
      ora: this.toIsoDateTime(value.data, value.ora),
      medicId: value.medicId ?? undefined,
      medic: resolvedMedic,
      biopsiiL1: value.biopsiiL1 || undefined,
      biopsiiN1: value.biopsiiN1 ?? undefined,
      nrap1: value.nrap1 ?? undefined,
      biopsiiR1: value.biopsiiR1 || undefined,
      data1: this.toIsoDate(value.data1),
      ora1: this.toIsoDateTime(value.data1, value.ora1),
      sedareT1: value.sedareT1 || undefined,
      dozaS: value.dozaS ?? undefined,
      medicatie1: value.medicatie1 || undefined,
      dozaM: value.dozaM ?? undefined,
      consumabile: value.consumabile || undefined,
      materiale: value.materiale || undefined,
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

  get medicDisplayName(): string {
    const byId = this.lookupMedicName(this.form.controls.medicId.value);
    return byId ?? this.form.controls.medic.value ?? '';
  }

}
