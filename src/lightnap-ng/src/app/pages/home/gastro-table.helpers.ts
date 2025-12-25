export interface GastroFilters {
  nr: string;
  nume: string;
  prenume: string;
  cnp: string;
}

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
