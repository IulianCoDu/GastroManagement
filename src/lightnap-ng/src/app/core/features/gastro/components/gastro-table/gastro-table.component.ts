import { CommonModule } from "@angular/common";
import { Component, Input } from "@angular/core";
import { PanelModule } from "primeng/panel";
import { ProgressSpinnerModule } from "primeng/progressspinner";
import { TableModule } from "primeng/table";

export interface GastroTableColumn {
  field: string;
  header: string;
  width?: string;
}

@Component({
  standalone: true,
  selector: "app-gastro-table",
  templateUrl: "./gastro-table.component.html",
  styleUrls: ["./gastro-table.component.css"],
  imports: [CommonModule, PanelModule, TableModule, ProgressSpinnerModule],
})
export class GastroTableComponent {
  @Input() title = "";
  @Input() columns: Array<GastroTableColumn> = [];
  @Input() data: Array<Record<string, unknown>> = [];
  @Input() loading = false;
  @Input() scrollHeight = "1000px";
}
