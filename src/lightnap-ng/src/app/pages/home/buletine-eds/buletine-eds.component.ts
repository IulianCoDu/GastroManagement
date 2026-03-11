import { Component } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { RouterLink } from "@angular/router";
import { PanelModule } from "primeng/panel";
import { ProgressSpinnerModule } from "primeng/progressspinner";
import { TableModule } from "primeng/table";
import { InputTextModule } from "primeng/inputtext";
import { ButtonModule } from "primeng/button";
import { GastroTableColumn } from "@core/features/gastro/components/gastro-table/gastro-table.component";
import { BuletinEdsDto } from "@core/backend-api/dtos";
import { ErrorListComponent } from "@core/components/error-list/error-list.component";
import { ConfirmPopupComponent } from "@core/components/confirm-popup/confirm-popup.component";
import { UserSettingKeys } from "@core/backend-api";
import { GastroChartConfig, GastroChartsComponent } from "@core/features/gastro/components/gastro-charts/gastro-charts.component";
import { BuletineBaseComponent } from "../buletine-base.component";

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
  templateUrl: "./buletine-eds.component.html",
  styleUrls: ["../gastro-table.page.css"],
})
export class BuletineEdsComponent extends BuletineBaseComponent<BuletinEdsDto> {
  readonly chartSettingsKey = UserSettingKeys.GastroChartsEds;
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
    { field: "esofag", header: "Esofag" },
    { field: "jonctiune", header: "Joncțiune" },
    { field: "stomac", header: "Stomac" },
    { field: "pilor", header: "Pilor" },
    { field: "bulb", header: "Bulb" },
    { field: "duoden", header: "Duoden" },
    { field: "biopsiiL", header: "Biopsii L" },
    { field: "biopsiiN", header: "Biopsii N" },
    { field: "nrap", header: "Nr. reg. AP" },
    { field: "biopsiiR", header: "Rezultate" },
    { field: "tratament", header: "Tratament" },
    { field: "data", header: "Data" },
    { field: "ora", header: "Ora" },
    { field: "medic", header: "Medic" },
  ];

  printRecord(record: BuletinEdsDto) {
    const today = new Date().toLocaleDateString('ro-RO', { day: '2-digit', month: '2-digit', year: 'numeric' });
    const f = (v: unknown) => String(v ?? '');
    this.doPrint(`
      <div class="bp-header">
        <img src="assets/images/logos/logo_UMF_200x200.png" class="bp-logo" alt="Logo" />
        <div>
          <div class="bp-clinic-name">CENTRUL MEDICAL GASTROENTEROLOGIE</div>
          <div class="bp-clinic-sub">Adresa centrului &bull; Tel: 0xxx xxx xxx</div>
        </div>
        <div class="bp-header-date">${today}</div>
      </div>
      <hr class="bp-divider" />
      <h1 class="bp-title">Buletin Endoscopie Digestivă Superioară Nr. ${f(record.nr)}</h1>
      <div class="bp-meta">Data: ${this.fmtDate(record.data)} &emsp; Ora: ${this.fmtTime(record.ora)}</div>
      <hr class="bp-divider" />
      <table class="bp-table-patient">
        <tr>
          <td class="bp-label">Pacient:</td>
          <td class="bp-value"><strong>${f(record.nume)} ${f(record.prenume)}</strong></td>
          <td class="bp-label">Vârsta:</td>
          <td class="bp-value">${f(record.virsta)} ani</td>
        </tr>
        <tr><td class="bp-label">CNP:</td><td class="bp-value" colspan="3">${f(record.cnp)}</td></tr>
        <tr><td class="bp-label">Domiciliu:</td><td class="bp-value" colspan="3">${f(record.domiciliu)}</td></tr>
        <tr><td class="bp-label">Diagnostic:</td><td class="bp-value" colspan="3">${f(record.diagnostic)}</td></tr>
        <tr>
          <td class="bp-label">Sedare:</td>
          <td class="bp-value">${f(record.sedareT)}</td>
          <td class="bp-label">Medicație:</td>
          <td class="bp-value">${f(record.medicatie)}</td>
        </tr>
      </table>
      <hr class="bp-divider" />
      <h2 class="bp-section-title">Examen Endoscopic</h2>
      <div class="bp-finding"><span class="bp-fl">Esofag:</span><span class="bp-fv">${f(record.esofag)}</span></div>
      <div class="bp-finding"><span class="bp-fl">Joncțiune esogastrică:</span><span class="bp-fv">${f(record.jonctiune)}</span></div>
      <div class="bp-finding"><span class="bp-fl">Stomac:</span><span class="bp-fv">${f(record.stomac)}</span></div>
      <div class="bp-finding"><span class="bp-fl">Pilor:</span><span class="bp-fv">${f(record.pilor)}</span></div>
      <div class="bp-finding"><span class="bp-fl">Bulb duodenal:</span><span class="bp-fv">${f(record.bulb)}</span></div>
      <div class="bp-finding"><span class="bp-fl">Duoden II:</span><span class="bp-fv">${f(record.duoden)}</span></div>
      <hr class="bp-divider-thin" />
      <h2 class="bp-section-title">Biopsii</h2>
      <div class="bp-finding-row">
        <div class="bp-finding"><span class="bp-fl">Localizare:</span><span class="bp-fv">${f(record.biopsiiL)}</span></div>
        <div class="bp-finding"><span class="bp-fl">Nr. fragmente:</span><span class="bp-fv">${f(record.biopsiiN)}</span></div>
        <div class="bp-finding"><span class="bp-fl">Nr. reg. AP:</span><span class="bp-fv">${f(record.nrap)}</span></div>
      </div>
      <div class="bp-finding"><span class="bp-fl">Rezultat histopatologic:</span><span class="bp-fv">${f(record.biopsiiR)}</span></div>
      <hr class="bp-divider-thin" />
      <div class="bp-finding"><span class="bp-fl">Tratament / Recomandări:</span><span class="bp-fv">${f(record.tratament)}</span></div>
      <div class="bp-footer">
        <div class="bp-signature">
          <div>Medic: Dr. ${f(record.medic)}</div>
          <div class="bp-sig-line">Semnătură și parafă</div>
        </div>
      </div>
    `);
  }

  protected getRecordsRequest() {
    return this.gastroService.getBuletineEds();
  }

  protected deleteRecordRequest(recordId: number) {
    return this.gastroService.deleteBuletineEds(recordId);
  }

  protected getDeleteConfirmKey(recordId: number) {
    return `buletin-eds-${recordId}`;
  }
}
