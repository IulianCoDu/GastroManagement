/*
 * Settings keys used in the application.
 */
export type UserSettingKey =
  | "BrowserSettings"
  | "PreferredLanguage"
  | "GastroChartsEco"
  | "GastroChartsEds"
  | "GastroChartsEdi";

export const UserSettingKeys = {
  BrowserSettings: "BrowserSettings",
  PreferredLanguage: "PreferredLanguage",
  GastroChartsEco: "GastroChartsEco",
  GastroChartsEds: "GastroChartsEds",
  GastroChartsEdi: "GastroChartsEdi",
} as const;
