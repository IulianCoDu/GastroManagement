import { AppRoute } from "@core";

export const Routes: AppRoute[] = [
  {
    path: "",
    title: "Administrare | Acasa",
    data: { alias: "admin-home", breadcrumb: "" },
    loadComponent: () => import("./index/index.component").then(m => m.IndexComponent),
  },
  {
    path: "users",
    data: { breadcrumb: "Utilizatori" },
    children: [
      {
        path: "",
        title: "Administrare | Utilizatori",
        data: { alias: "admin-users", breadcrumb: "" },
        loadComponent: () => import("./users/users.component").then(m => m.UsersComponent),
      },
      {
        path: "users/:userName",
        title: "Administrare | Utilizator",
        data: {
          alias: "admin-user",
          breadcrumb: (route) => route.params["userName"] || "Detalii utilizator"
        },
        loadComponent: () => import("./user/user.component").then(m => m.UserComponent),
      },
    ],
  },
  {
    path: "roles",
    data: { breadcrumb: "Roluri" },
    children: [
      {
        path: "",
        title: "Administrare | Roluri",
        data: { alias: "admin-roles", breadcrumb: "" },
        loadComponent: () => import("./roles/roles.component").then(m => m.RolesComponent),
      },
      {
        path: "roles/:role",
        title: "Administrare | Rol",
        data: {
          alias: "admin-role",
          breadcrumb: (route) => route.params["role"] || "Detalii rol"
        },
        loadComponent: () => import("./role/role.component").then(m => m.RoleComponent),
      },
    ],
  },
  {
    path: "claims",
    data: { breadcrumb: "Permisiuni" },
    children: [
      {
        path: "",
        title: "Administrare | Permisiuni",
        data: { alias: "admin-claims", breadcrumb: "" },
        loadComponent: () => import("./claims/claims.component").then(m => m.ClaimsComponent),
      },
      {
        path: "claims/:type/:value",
        title: "Administrare | Permisiune",
        data: {
          alias: "admin-claim",
          breadcrumb: (route) => {
            const type = route.params["type"];
            const value = route.params["value"];
            return type && value ? `${type}: ${value}` : "Detalii permisiune";
          }
        },
        loadComponent: () => import("./claim/claim.component").then(m => m.ClaimComponent),
      },
    ],
  },
];
