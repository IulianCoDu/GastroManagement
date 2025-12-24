import { AppRoute } from "@core";

export const Routes: AppRoute[] = [
  {
    path: "",
    title: "Profil | Acasa",
    data: { alias: "profile", breadcrumb: "" },
    loadComponent: () => import("./index/index.component").then(m => m.IndexComponent),
  },
  {
    path: "devices",
    title: "Profil | Dispozitive",
    data: { alias: "devices", breadcrumb: "Dispozitive" },
    loadComponent: () => import("./devices/devices.component").then(m => m.DevicesComponent),
  },
  {
    path: "notifications",
    title: "Profil | Notificari",
    data: { alias: "notifications", breadcrumb: "Notificari" },
    loadComponent: () => import("./notifications/notifications.component").then(m => m.NotificationsComponent),
  },
  {
    path: "change-password",
    title: "Profil | Schimba parola",
    data: { alias: "change-password", breadcrumb: "Schimba parola" },
    loadComponent: () => import("./change-password/change-password.component").then(m => m.ChangePasswordComponent),
  },
  {
    path: "change-email",
    title: "Profil | Schimba email",
    data: { alias: "change-email", breadcrumb: "Schimba email" },
    loadComponent: () => import("./change-email/change-email.component").then(m => m.ChangeEmailComponent),
  },
  {
    path: "change-email-requested",
    title: "Profil | Schimbare email solicitata",
    data: { alias: "change-email-requested", breadcrumb: "Schimbare email solicitata" },
    loadComponent: () => import("./change-email-requested/change-email-requested.component").then(m => m.ChangeEmailRequestedComponent),
  },
  {
    path: "confirm-email-change/:newEmail/:code",
    title: "Profil | Confirmare schimbare email",
    data: { breadcrumb: "Confirmare schimbare email" },
    loadComponent: () => import("./confirm-email-change/confirm-email-change.component").then(m => m.ConfirmEmailChangeComponent),
  },
];
