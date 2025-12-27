import { AppRoute } from "@core";

export const Routes: AppRoute[] = [
  { path: "", title: "User | Home", data: { alias: "user-home", breadcrumb: "" }, loadComponent: () => import("./index/index.component").then(m => m.IndexComponent) },
  {
    path: "buletine-eco",
    data: { alias: "buletine-eco", breadcrumb: "Buletine ECO" },
    children: [
      {
        path: "new",
        title: "Home | Buletine ECO | Nou",
        data: { alias: "buletine-eco-new", breadcrumb: "Nou" },
        loadComponent: () => import("./buletine-eco/buletine-eco-edit.component").then(m => m.BuletineEcoEditComponent),
      },
      {
        path: ":id",
        title: "Home | Buletine ECO | Editare",
        data: { alias: "buletine-eco-edit", breadcrumb: "Editare" },
        loadComponent: () => import("./buletine-eco/buletine-eco-edit.component").then(m => m.BuletineEcoEditComponent),
      },
      {
        path: "",
        title: "Home | Buletine ECO",
        loadComponent: () => import("./buletine-eco/buletine-eco.component").then(m => m.BuletineEcoComponent),
      },
    ],
  },
  {
    path: "buletine-eds",
    data: { alias: "buletine-eds", breadcrumb: "Buletine EDS" },
    children: [
      {
        path: "new",
        title: "Home | Buletine EDS | Nou",
        data: { alias: "buletine-eds-new", breadcrumb: "Nou" },
        loadComponent: () => import("./buletine-eds/buletine-eds-edit.component").then(m => m.BuletineEdsEditComponent),
      },
      {
        path: ":id",
        title: "Home | Buletine EDS | Editare",
        data: { alias: "buletine-eds-edit", breadcrumb: "Editare" },
        loadComponent: () => import("./buletine-eds/buletine-eds-edit.component").then(m => m.BuletineEdsEditComponent),
      },
      {
        path: "",
        title: "Home | Buletine EDS",
        loadComponent: () => import("./buletine-eds/buletine-eds.component").then(m => m.BuletineEdsComponent),
      },
    ],
  },
  {
    path: "buletine-edi",
    data: { alias: "buletine-edi", breadcrumb: "Buletine EDI" },
    children: [
      {
        path: "new",
        title: "Home | Buletine EDI | Nou",
        data: { alias: "buletine-edi-new", breadcrumb: "Nou" },
        loadComponent: () => import("./buletine-edi/buletine-edi-edit.component").then(m => m.BuletineEdiEditComponent),
      },
      {
        path: ":id",
        title: "Home | Buletine EDI | Editare",
        data: { alias: "buletine-edi-edit", breadcrumb: "Editare" },
        loadComponent: () => import("./buletine-edi/buletine-edi-edit.component").then(m => m.BuletineEdiEditComponent),
      },
      {
        path: "",
        title: "Home | Buletine EDI",
        loadComponent: () => import("./buletine-edi/buletine-edi.component").then(m => m.BuletineEdiComponent),
      },
    ],
  },
];
