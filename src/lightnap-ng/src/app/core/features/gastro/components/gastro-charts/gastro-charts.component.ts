import { CommonModule } from "@angular/common";
import { Component, DestroyRef, OnInit, computed, effect, inject, input, signal } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";
import { UserSettingKey } from "@core/backend-api";
import { ListItem } from "@core/models";
import { ProfileService } from "@core/services/profile.service";
import { ToastService } from "@core/services/toast.service";
import { ChartData, ChartOptions } from "chart.js";
import { NgChartsModule } from "ng2-charts";
import { ButtonModule } from "primeng/button";
import { InputTextModule } from "primeng/inputtext";
import { PanelModule } from "primeng/panel";
import { Select } from "primeng/select";

export type GastroChartMode = "value" | "age-range";

export interface GastroChartConfig {
  id: string;
  title: string;
  field: string;
  mode: GastroChartMode;
  maxItems: number;
}

@Component({
  selector: "ln-gastro-charts",
  standalone: true,
  imports: [CommonModule, FormsModule, PanelModule, Select, InputTextModule, ButtonModule, NgChartsModule],
  templateUrl: "./gastro-charts.component.html",
  styleUrls: ["./gastro-charts.component.css"],
})
export class GastroChartsComponent implements OnInit {
  readonly #profileService = inject(ProfileService);
  readonly #toast = inject(ToastService);
  readonly #destroyRef = inject(DestroyRef);

  readonly records = input.required<Array<Record<string, unknown>>>();
  readonly settingsKey = input.required<UserSettingKey>();
  readonly defaultCharts = input<Array<GastroChartConfig>>([]);

  readonly charts = signal<Array<GastroChartConfig>>([]);
  readonly newTitle = signal("");
  readonly newField = signal("");
  readonly newMode = signal<GastroChartMode>("value");
  readonly newMaxItems = signal(12);

  readonly chartDataById = computed(() => {
    const records = this.records();
    const chartData = new Map<string, ChartData<"pie">>();
    this.charts().forEach(chart => {
      chartData.set(chart.id, this.#buildChartData(chart, records));
    });
    return chartData;
  });

  readonly emptyChartData: ChartData<"pie"> = {
    labels: [],
    datasets: [
      {
        data: [],
      },
    ],
  };

  readonly fieldOptions = computed(() => {
    const records = this.records();
    if (!records.length) return [];

    const keys = Object.keys(records[0] ?? {}).sort((a, b) => a.localeCompare(b));
    return keys.map(key => new ListItem(key, this.#labelize(key)));
  });

  readonly modeOptions = computed(() => {
    const field = this.newField();
    const items = [new ListItem<GastroChartMode>("value", "Valori unice")];
    if (field === "virsta") {
      items.push(new ListItem<GastroChartMode>("age-range", "Intervale varsta (0-10, 10-20...)"));
    }
    return items;
  });

  readonly chartOptions: ChartOptions<"pie"> = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        position: "right",
      },
    },
  };

  constructor() {
    effect(() => {
      if (!this.newField() && this.fieldOptions().length) {
        const defaultField = this.fieldOptions()[0].value;
        this.newField.set(defaultField);
        this.newMode.set(defaultField === "virsta" ? "age-range" : "value");
      }
    });
  }

  ngOnInit(): void {
    this.#profileService
      .getSetting<Array<GastroChartConfig>>(this.settingsKey(), this.defaultCharts())
      .pipe(takeUntilDestroyed(this.#destroyRef))
      .subscribe({
        next: charts => {
          const normalized = charts.length ? charts : this.defaultCharts();
          this.charts.set(normalized);
        },
        error: () => {
          this.charts.set(this.defaultCharts());
        },
      });
  }

  addChart() {
    const field = this.newField();
    if (!field.length) return;

    const mode = this.newMode();
    const title = this.newTitle().trim() || this.#defaultTitle(field, mode);
    const newChart: GastroChartConfig = {
      id: this.#createId(),
      title,
      field,
      mode,
      maxItems: this.newMaxItems(),
    };

    this.charts.set([...this.charts(), newChart]);
    this.newTitle.set("");
    this.#saveCharts();
  }

  onFieldChange(field: string) {
    this.newField.set(field);
    this.newMode.set(field === "virsta" ? "age-range" : "value");
  }

  onModeChange(mode: GastroChartMode) {
    this.newMode.set(mode);
  }

  onTitleChange(title: string) {
    this.newTitle.set(title);
  }

  onMaxItemsChange(value: number | string) {
    const parsed = Number(value);
    this.newMaxItems.set(Number.isFinite(parsed) ? parsed : 0);
  }

  removeChart(chartId: string) {
    this.charts.set(this.charts().filter(chart => chart.id !== chartId));
    this.#saveCharts();
  }

  #buildChartData(chart: GastroChartConfig, records: Array<Record<string, unknown>>): ChartData<"pie"> {
    const values = new Map<string, number>();

    records.forEach(record => {
      const rawValue = record[chart.field];
      const label = chart.mode === "age-range" ? this.#ageRangeLabel(rawValue) : this.#valueLabel(rawValue);
      values.set(label, (values.get(label) ?? 0) + 1);
    });

    let entries = Array.from(values.entries()).sort((a, b) => b[1] - a[1]);
    if (chart.maxItems > 0) {
      entries = entries.slice(0, chart.maxItems);
    }

    return {
      labels: entries.map(entry => entry[0]),
      datasets: [
        {
          data: entries.map(entry => entry[1]),
        },
      ],
    };
  }

  #saveCharts() {
    this.#profileService.setSetting(this.settingsKey(), this.charts()).pipe(takeUntilDestroyed(this.#destroyRef)).subscribe({
      error: () => {
        this.#toast.error("Nu s-au putut salva graficele.");
      },
    });
  }

  #ageRangeLabel(value: unknown) {
    const numeric = typeof value === "number" ? value : Number(value);
    if (!Number.isFinite(numeric) || numeric < 0) return "N/A";
    if (numeric >= 100) return "90-100";

    const start = Math.floor(numeric / 10) * 10;
    const end = start + 10;
    return `${start}-${end}`;
  }

  #valueLabel(value: unknown) {
    if (value === null || value === undefined || value === "") return "N/A";
    if (typeof value === "boolean") return value ? "Da" : "Nu";
    return String(value);
  }

  #labelize(key: string) {
    return key
      .replace(/([A-Z])/g, " $1")
      .replace(/^./, char => char.toUpperCase())
      .replace(/_/g, " ");
  }

  #defaultTitle(field: string, mode: GastroChartMode) {
    if (mode === "age-range") return "Distributie varsta";
    return `Distributie dupa ${this.#labelize(field).toLowerCase()}`;
  }

  #createId() {
    return `${Date.now()}-${Math.random().toString(16).slice(2)}`;
  }
}
