export interface GastroFilters {
  nume: string;
  prenume: string;
}

export function applyGastroFilters<T extends { nume?: string; prenume?: string }>(
  records: Array<T>,
  filters: GastroFilters
): Array<T> {
  if (!records.length) {
    return records;
  }

  const numeFilter = (filters.nume ?? "").trim().toLowerCase();
  const prenumeFilter = (filters.prenume ?? "").trim().toLowerCase();

  if (!numeFilter && !prenumeFilter) {
    return records;
  }

  return records.filter(record => {
    const normalizedNume = (record.nume ?? "").toString().toLowerCase();
    const normalizedPrenume = (record.prenume ?? "").toString().toLowerCase();

    const matchesNume = !numeFilter || normalizedNume.includes(numeFilter);
    const matchesPrenume = !prenumeFilter || normalizedPrenume.includes(prenumeFilter);

    return matchesNume && matchesPrenume;
  });
}
