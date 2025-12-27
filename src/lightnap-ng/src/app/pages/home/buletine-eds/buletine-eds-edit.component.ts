import { CommonModule } from "@angular/common";
import { Component, DestroyRef, OnInit, computed, inject, input, signal } from "@angular/core";
import { FormBuilder, ReactiveFormsModule } from "@angular/forms";
import { RouterLink } from "@angular/router";
import { CreateBuletinEdsDto, UpdateBuletinEdsDto, setApiErrors } from "@core";
import { ErrorListComponent } from "@core/components/error-list/error-list.component";
import { GastroDataService } from "@core/backend-api/services/gastro-data.service";
import { RouteAliasService } from "@core/features/routing/services/route-alias-service";
import { ToastService } from "@core/services/toast.service";
import { ButtonModule } from "primeng/button";
import { InputTextModule } from "primeng/inputtext";
import { PanelModule } from "primeng/panel";
import { ProgressSpinnerModule } from "primeng/progressspinner";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";
import { BuletineEditBaseComponent } from "../buletine-edit-base.component";

@Component({
  standalone: true,
  templateUrl: "./buletine-eds-edit.component.html",
  imports: [
    CommonModule,
    ReactiveFormsModule,
    PanelModule,
    InputTextModule,
    ButtonModule,
    ProgressSpinnerModule,
    ErrorListComponent,
    RouterLink,
  ],
})
export class BuletineEdsEditComponent extends BuletineEditBaseComponent implements OnInit {
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
  readonly #applyApiErrors = setApiErrors(this.errors);

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
    esofag: this.#fb.control(""),
    jonctiune: this.#fb.control(""),
    stomac: this.#fb.control(""),
    pilor: this.#fb.control(""),
    bulb: this.#fb.control(""),
    duoden: this.#fb.control(""),
    biopsiiL: this.#fb.control(""),
    biopsiiN: this.#fb.control<number | null>(null),
    nrap: this.#fb.control<number | null>(null),
    biopsiiR: this.#fb.control(""),
    tratament: this.#fb.control(""),
    data: this.#fb.control(""),
    ora: this.#fb.control(""),
    medic: this.#fb.control(""),
  });

  ngOnInit() {
    const id = this.recordId();
    if (!id) return;

    this.loading.set(true);
    this.#gastroService
      .getBuletinEds(id)
      .pipe(takeUntilDestroyed(this.#destroyRef))
      .subscribe({
        next: record => {
          if (record) {
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
              esofag: record.esofag ?? "",
              jonctiune: record.jonctiune ?? "",
              stomac: record.stomac ?? "",
              pilor: record.pilor ?? "",
              bulb: record.bulb ?? "",
              duoden: record.duoden ?? "",
              biopsiiL: record.biopsiiL ?? "",
              biopsiiN: record.biopsiiN ?? null,
              nrap: record.nrap ?? null,
              biopsiiR: record.biopsiiR ?? "",
              tratament: record.tratament ?? "",
              data: this.formatDate(record.data),
              ora: this.formatTime(record.ora),
              medic: record.medic ?? "",
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
      this.#gastroService.updateBuletinEds(id, payload).subscribe({
        next: () => {
          this.#toast.success("Buletin EDS a fost actualizat.");
          this.#routeAlias.navigate("buletine-eds");
        },
        error: this.#applyApiErrors,
      });
      return;
    }

    this.#gastroService.createBuletinEds(payload).subscribe({
      next: () => {
        this.#toast.success("Buletin EDS a fost creat.");
        this.form.reset();
        this.#routeAlias.navigate("buletine-eds");
      },
      error: this.#applyApiErrors,
    });
  }

  private buildPayload(): CreateBuletinEdsDto & UpdateBuletinEdsDto {
    const value = this.form.getRawValue();
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
      esofag: value.esofag || undefined,
      jonctiune: value.jonctiune || undefined,
      stomac: value.stomac || undefined,
      pilor: value.pilor || undefined,
      bulb: value.bulb || undefined,
      duoden: value.duoden || undefined,
      biopsiiL: value.biopsiiL || undefined,
      biopsiiN: value.biopsiiN ?? undefined,
      nrap: value.nrap ?? undefined,
      biopsiiR: value.biopsiiR || undefined,
      tratament: value.tratament || undefined,
      data: this.toIsoDate(value.data),
      ora: this.toIsoDateTime(value.data, value.ora),
      medic: value.medic || undefined,
    };
  }

}
