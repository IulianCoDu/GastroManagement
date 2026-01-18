export interface BuletinEdsDto {
  id: number;
  nr?: number;
  nume?: string;
  prenume?: string;
  virsta?: number;
  cnp?: string;
  domiciliu?: string;
  diagnostic?: string;
  sistem: string;
  sedareT?: string;
  medicatie?: string;
  esofag?: string;
  jonctiune?: string;
  stomac?: string;
  pilor?: string;
  bulb?: string;
  duoden?: string;
  biopsiiL?: string;
  biopsiiN?: number;
  nrap?: number;
  biopsiiR?: string;
  tratament?: string;
  data?: string;
  ora?: string;
  medicId?: number | null;
  medic?: string;
  [key: string]: unknown;
}
