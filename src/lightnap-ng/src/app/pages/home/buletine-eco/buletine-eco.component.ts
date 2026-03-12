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
import { BuletinEcoDto } from "@core/backend-api/dtos";
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
  templateUrl: "./buletine-eco.component.html",
  styleUrls: ["../gastro-table.page.css"],
})
export class BuletineEcoComponent extends BuletineBaseComponent<BuletinEcoDto> {
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
    { field: "vp", header: "Vena Portă" },
    { field: "vs", header: "Vena Splenică" },
    { field: "cbp", header: "Căi Biliare Principale" },
    { field: "pancreas", header: "Pancreas" },
    { field: "splina", header: "Splină" },
    { field: "rd", header: "Rinichi Drept" },
    { field: "rs", header: "Rinichi Stâng" },
    { field: "vu", header: "Vezica Urinară" },
    { field: "prostata", header: "Prostată" },
    { field: "ogi", header: "OGI" },
    { field: "obs", header: "Observații" },
    { field: "data", header: "Data" },
    { field: "ora", header: "Ora" },
    { field: "medic", header: "Medic" },
    { field: "ceus", header: "CE-US" },
    { field: "hasFig1", header: "Fig1" },
    { field: "hasFig2", header: "Fig2" },
    { field: "hasFig3", header: "Fig3" },
    { field: "hasFilm1", header: "Film" },
  ];

  printRecord(record: BuletinEcoDto) {
    const today = new Date().toLocaleDateString("ro-RO", { day: "2-digit", month: "2-digit", year: "numeric" });
    const f = (v: unknown) => String(v ?? "");
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
      <h1 class="bp-title">Buletin Ecografic Nr. ${f(record.nr)}</h1>
      <div class="bp-meta">Data: ${this.fmtDate(record.data)} &emsp; Ora: ${this.fmtTime(record.ora)}</div>
      <hr class="bp-divider" />
      <table class="bp-table-patient">
        <tr>
          <td class="bp-label">Pacient:</td>
          <td class="bp-value"><strong>${f(record.nume)} ${f(record.prenume)}</strong></td>
          <td class="bp-label">Vârsta:</td>
          <td class="bp-value">${f(record.virsta)} ani</td>
        </tr>
        <tr>
          <td class="bp-label">CNP:</td>
          <td class="bp-value">${f(record.cnp)}</td>
          <td class="bp-label">Telefon:</td>
          <td class="bp-value">${f(record.telefon)}</td>
        </tr>
        <tr><td class="bp-label">Domiciliu:</td><td class="bp-value" colspan="3">${f(record.domiciliu)}</td></tr>
        <tr><td class="bp-label">Diagnostic:</td><td class="bp-value" colspan="3">${f(record.diagnostic)}</td></tr>
      </table>
      <hr class="bp-divider" />
      <h2 class="bp-section-title">Examen Ecografic Abdominal</h2>
      <div class="bp-finding"><span class="bp-fl">Ficat:</span><span class="bp-fv">${f(record.ficat)}</span></div>
      <div class="bp-finding-row">
        <div class="bp-finding"><span class="bp-fl">Vena Portă:</span><span class="bp-fv">${f(record.vp)} mm</span></div>
        <div class="bp-finding"><span class="bp-fl">Venă Splenică:</span><span class="bp-fv">${f(record.vs)} mm</span></div>
        <div class="bp-finding"><span class="bp-fl">Căi Biliare Principale:</span><span class="bp-fv">${f(record.cbp)} mm</span></div>
      </div>
      <div class="bp-finding"><span class="bp-fl">Colecist:</span><span class="bp-fv">${f(record.colecist)}</span></div>
      <div class="bp-finding"><span class="bp-fl">Pancreas:</span><span class="bp-fv">${f(record.pancreas)}</span></div>
      <div class="bp-finding"><span class="bp-fl">Splină:</span><span class="bp-fv">${f(record.splina)}</span></div>
      <div class="bp-finding"><span class="bp-fl">Rinichi Drept:</span><span class="bp-fv">${f(record.rd)}</span></div>
      <div class="bp-finding"><span class="bp-fl">Rinichi Stâng:</span><span class="bp-fv">${f(record.rs)}</span></div>
      <div class="bp-finding"><span class="bp-fl">Vezică Urinară:</span><span class="bp-fv">${f(record.vu)}</span></div>
      <div class="bp-finding"><span class="bp-fl">Prostată:</span><span class="bp-fv">${f(record.prostata)}</span></div>
      <div class="bp-finding"><span class="bp-fl">OGI:</span><span class="bp-fv">${f(record.ogi)}</span></div>
      <hr class="bp-divider-thin" />
      <div class="bp-finding"><span class="bp-fl">Observații:</span><span class="bp-fv">${f(record.obs)}</span></div>
      <hr class="bp-divider-thin" />
      <div class="bp-finding"><span class="bp-fl">CE-US</span><span class="bp-fv">${f(record.ceus)}</span></div>
      <div class="bp-footer">
        <div class="bp-signature">
          <div>Medic: ${f(record.medic)}</div>
          <div class="bp-sig-line">Semnătură și parafă</div>
        </div>
      </div>
    `);
  }

  protected getRecordsRequest() {
    return this.gastroService.getBuletineEco();
  }

  protected deleteRecordRequest(recordId: number) {
    return this.gastroService.deleteBuletineEco(recordId);
  }

  protected getDeleteConfirmKey(recordId: number) {
    return `buletin-eco-${recordId}`;
  }
}
