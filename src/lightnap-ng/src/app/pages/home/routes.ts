import { AppRoute } from "@core";

export const Routes: AppRoute[] = [
  { path: "", title: "User | Home", data: { alias: "user-home", breadcrumb: "" }, loadComponent: () => import("./index/index.component").then(m => m.IndexComponent) },
  {
    path: "buletine-eco/new",
    title: "Home | Buletine ECO | Nou",
    data: { alias: "buletine-eco-new", breadcrumb: "Buletine ECO | Nou" },
    loadComponent: () => import("./buletine-eco/buletine-eco-edit.component").then(m => m.BuletineEcoEditComponent),
  },
  {
    path: "buletine-eco/:id",
    title: "Home | Buletine ECO | Editare",
    data: { alias: "buletine-eco-edit", breadcrumb: "Buletine ECO | Editare" },
    loadComponent: () => import("./buletine-eco/buletine-eco-edit.component").then(m => m.BuletineEcoEditComponent),
  },
  {
    path: "buletine-eco",
    title: "Home | Buletine ECO",
    data: { alias: "buletine-eco", breadcrumb: "Buletine ECO" },
    loadComponent: () => import("./buletine-eco/buletine-eco.component").then(m => m.BuletineEcoComponent),
  },
  {
    path: "buletine-eds/new",
    title: "Home | Buletine EDS | Nou",
    data: { alias: "buletine-eds-new", breadcrumb: "Buletine EDS | Nou" },
    loadComponent: () => import("./buletine-eds/buletine-eds-edit.component").then(m => m.BuletineEdsEditComponent),
  },
  {
    path: "buletine-eds/:id",
    title: "Home | Buletine EDS | Editare",
    data: { alias: "buletine-eds-edit", breadcrumb: "Buletine EDS | Editare" },
    loadComponent: () => import("./buletine-eds/buletine-eds-edit.component").then(m => m.BuletineEdsEditComponent),
  },
  {
    path: "buletine-eds",
    title: "Home | Buletine EDS",
    data: { alias: "buletine-eds", breadcrumb: "Buletine EDS" },
    loadComponent: () => import("./buletine-eds/buletine-eds.component").then(m => m.BuletineEdsComponent),
  },
  {
    path: "buletine-edi/new",
    title: "Home | Buletine EDI | Nou",
    data: { alias: "buletine-edi-new", breadcrumb: "Buletine EDI | Nou" },
    loadComponent: () => import("./buletine-edi/buletine-edi-edit.component").then(m => m.BuletineEdiEditComponent),
  },
  {
    path: "buletine-edi/:id",
    title: "Home | Buletine EDI | Editare",
    data: { alias: "buletine-edi-edit", breadcrumb: "Buletine EDI | Editare" },
    loadComponent: () => import("./buletine-edi/buletine-edi-edit.component").then(m => m.BuletineEdiEditComponent),
  },
  {
    path: "buletine-edi",
    title: "Home | Buletine EDI",
    data: { alias: "buletine-edi", breadcrumb: "Buletine EDI" },
    loadComponent: () => import("./buletine-edi/buletine-edi.component").then(m => m.BuletineEdiComponent),
  },
];
