export abstract class BuletineEditBaseComponent {
  protected toIsoDate(value: unknown) {
    const raw = (value ?? "").toString().trim();
    if (!raw) return undefined;
    if (raw.includes("T")) return raw;
    return `${raw}T00:00:00`;
  }

  protected toIsoDateTime(dateValue: unknown, timeValue: unknown) {
    const rawTime = (timeValue ?? "").toString().trim();
    if (!rawTime) return undefined;
    const rawDate = (dateValue ?? "").toString().trim();
    const datePart = rawDate ? rawDate : "1970-01-01";
    const timePart = rawTime.length === 5 ? `${rawTime}:00` : rawTime;
    return `${datePart}T${timePart}`;
  }

  protected formatDate(value: unknown) {
    if (value instanceof Date && !Number.isNaN(value.getTime())) {
      return this.formatDateParts(value.getDate(), value.getMonth() + 1, value.getFullYear());
    }
    const raw = (value ?? "").toString().trim();
    if (!raw) return "";
    const isoMatch = raw.match(/^(\d{4})-(\d{2})-(\d{2})/);
    if (isoMatch) return `${isoMatch[1]}-${isoMatch[2]}-${isoMatch[3]}`;
    const roMatch = raw.match(/^(\d{2})\/(\d{2})\/(\d{4})/);
    if (roMatch) return `${roMatch[3]}-${roMatch[2]}-${roMatch[1]}`;
    return raw;
  }

  protected formatTime(value: unknown) {
    if (value instanceof Date && !Number.isNaN(value.getTime())) {
      return `${this.pad2(value.getHours())}:${this.pad2(value.getMinutes())}:${this.pad2(value.getSeconds())}`;
    }
    const raw = (value ?? "").toString().trim();
    if (!raw) return "";
    const isoDateTime = raw.match(/T(\d{2}:\d{2}:\d{2})/);
    if (isoDateTime) return isoDateTime[1];
    const timeMatch = raw.match(/\b(\d{2}:\d{2}:\d{2})\b/);
    if (timeMatch) return timeMatch[1];
    const shortMatch = raw.match(/^(\d{2}:\d{2})$/);
    if (shortMatch) return `${shortMatch[1]}:00`;
    return raw;
  }

  private formatDateParts(day: number, month: number, year: number) {
    return `${year}-${this.pad2(month)}-${this.pad2(day)}`;
  }

  private pad2(value: number) {
    return value.toString().padStart(2, "0");
  }
}
