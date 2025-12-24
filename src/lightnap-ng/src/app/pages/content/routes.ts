import { AppRoute, RoleNames } from "@core";
import { AppLayoutComponent } from "@core/features/layout/components/layouts/app-layout/app-layout.component";
import { PublicLayoutComponent } from "@core/features/layout/components/layouts/public-layout/public-layout.component";
import { editPageGuard } from "@core/guards/edit-page.guard";
import { permissionsGuard } from "@core/guards/permissions.guard";
import { readPageGuard } from "@core/guards/read-page.guard";

export const Routes: AppRoute[] = [
  {
    path: "",
    component: AppLayoutComponent,
    data: { breadcrumb: "Continut" },
    children: [
      {
        path: "",
        canActivate: [permissionsGuard([RoleNames.Administrator, RoleNames.ContentEditor], [])],
        data: { alias: "manage-content", breadcrumb: "" },
        title: "Gestioneaza continutul",
        loadComponent: () => import("./manage/manage.component").then(m => m.ManageComponent),
      },
      {
        path: "edit/:key",
        data: {
          breadcrumb: route => route.params["key"] || "Editare",
        },
        children: [
          {
            path: "",
            data: { alias: "edit-content", breadcrumb: "" },
            canActivate: [editPageGuard],
            title: "Editeaza continut",
            loadComponent: () => import("./edit/edit.component").then(m => m.EditComponent),
          },
          {
            path: ":languageCode",
            data: {
              alias: "edit-language",
              breadcrumb: route => route.params["languageCode"] || "Limba",
            },
            canActivate: [editPageGuard],
            title: "Editeaza limba",
            loadComponent: () => import("./edit-language/edit-language.component").then(m => m.EditLanguageComponent),
          },
        ],
      },
    ],
  },
  {
    path: "",
    component: PublicLayoutComponent,
    children: [
      {
        path: ":key",
        canActivate: [readPageGuard],
        data: { alias: "view-content" },
        title: "Vizualizeaza continutul",
        loadComponent: () => import("./page/page.component").then(m => m.PageComponent),
      },
    ],
  },
];
