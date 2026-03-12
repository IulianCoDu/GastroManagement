export function openPrintTab(html: string) {
  const links = Array.from(document.head.querySelectorAll<HTMLLinkElement>('link[rel="stylesheet"]'))
    .map(l => `<link rel="stylesheet" href="${l.href}">`)
    .join('\n');

  const fullHtml = `<!DOCTYPE html><html>
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <base href="${document.baseURI}">
  ${links}
  <style>
    body { margin: 0; padding: 0; background: #fff; }
    .bp-page { display: block !important; width: 100%; background: #fff; color: #000; font-family: 'Times New Roman', Times, serif; font-size: 11pt; padding: 15mm 20mm; box-sizing: border-box; }
  </style>
</head>
<body>
  <div class="bp-page">${html}</div>
  <script>window.onload = function() { setTimeout(function() { window.print(); }, 300); };<\/script>
</body>
</html>`;

  const blob = new Blob([fullHtml], { type: 'text/html' });
  const url = URL.createObjectURL(blob);
  window.open(url, '_blank');
  setTimeout(() => URL.revokeObjectURL(url), 60_000);
}

export interface GastroFilters {
  nr: string;
  nume: string;
  prenume: string;
  cnp: string;
}

const DATE_FIELDS = new Set(["data", "data1"]);
const TIME_FIELDS = new Set(["ora", "ora1"]);

export function applyGastroFilters<T extends { nr?: number | string; nume?: string; prenume?: string; cnp?: string }>(
  records: Array<T>,
  filters: GastroFilters
): Array<T> {
  if (!records.length) {
    return records;
  }

  const nrFilter = (filters.nr ?? "").trim().toLowerCase();
  const numeFilter = (filters.nume ?? "").trim().toLowerCase();
  const prenumeFilter = (filters.prenume ?? "").trim().toLowerCase();
  const cnpFilter = (filters.cnp ?? "").trim().toLowerCase();

  if (!nrFilter && !numeFilter && !prenumeFilter && !cnpFilter) {
    return records;
  }

  return records.filter(record => {
    const normalizedNr = (record.nr ?? "").toString().toLowerCase();
    const normalizedNume = (record.nume ?? "").toString().toLowerCase();
    const normalizedPrenume = (record.prenume ?? "").toString().toLowerCase();
    const normalizedCnp = (record.cnp ?? "").toString().toLowerCase();

    const matchesNr = !nrFilter || normalizedNr.includes(nrFilter);
    const matchesNume = !numeFilter || normalizedNume.includes(numeFilter);
    const matchesPrenume = !prenumeFilter || normalizedPrenume.includes(prenumeFilter);
    const matchesCnp = !cnpFilter || normalizedCnp.includes(cnpFilter);

    return matchesNr && matchesNume && matchesPrenume && matchesCnp;
  });
}

export function formatGastroCell(field: string, value: unknown): string | number | boolean {
  if (value === null || value === undefined || value === "") return "-";
  if (DATE_FIELDS.has(field)) return formatGastroDate(value);
  if (TIME_FIELDS.has(field)) return formatGastroTime(value);
  if (typeof value === "string" || typeof value === "number" || typeof value === "boolean") return value;
  return String(value);
}

function formatGastroDate(value: unknown): string {
  if (value instanceof Date && !Number.isNaN(value.getTime())) {
    return formatDateParts(value.getDate(), value.getMonth() + 1, value.getFullYear());
  }
  const raw = String(value).trim();
  if (!raw) return "-";
  const isoMatch = raw.match(/^(\d{4})-(\d{2})-(\d{2})/);
  if (isoMatch) return `${isoMatch[3]}/${isoMatch[2]}/${isoMatch[1]}`;
  const roMatch = raw.match(/^(\d{2})\/(\d{2})\/(\d{4})/);
  if (roMatch) return `${roMatch[1]}/${roMatch[2]}/${roMatch[3]}`;
  return raw;
}

function formatGastroTime(value: unknown): string {
  if (value instanceof Date && !Number.isNaN(value.getTime())) {
    return `${pad2(value.getHours())}:${pad2(value.getMinutes())}:${pad2(value.getSeconds())}`;
  }
  const raw = String(value).trim();
  if (!raw) return "-";
  const isoDateTime = raw.match(/T(\d{2}:\d{2}:\d{2})/);
  if (isoDateTime) return isoDateTime[1];
  const timeMatch = raw.match(/\b(\d{2}:\d{2}:\d{2})\b/);
  if (timeMatch) return timeMatch[1];
  const shortMatch = raw.match(/^(\d{2}:\d{2})$/);
  if (shortMatch) return `${shortMatch[1]}:00`;
  return raw;
}

function formatDateParts(day: number, month: number, year: number): string {
  return `${pad2(day)}/${pad2(month)}/${year}`;
}

function pad2(value: number): string {
  return value.toString().padStart(2, "0");
}
