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
