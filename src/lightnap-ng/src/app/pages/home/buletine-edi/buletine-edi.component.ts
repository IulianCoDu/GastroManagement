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
import { BuletinEdiDto } from "@core/backend-api/dtos";
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
  templateUrl: "./buletine-edi.component.html",
  styleUrls: ["../gastro-table.page.css"],
})
export class BuletineEdiComponent extends BuletineBaseComponent<BuletinEdiDto> {
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

  protected getRecordsRequest() {
    return this.gastroService.getBuletineEdi();
  }

  protected deleteRecordRequest(recordId: number) {
    return this.gastroService.deleteBuletineEdi(recordId);
  }

  protected getDeleteConfirmKey(recordId: number) {
    return `buletin-edi-${recordId}`;
  }
}
