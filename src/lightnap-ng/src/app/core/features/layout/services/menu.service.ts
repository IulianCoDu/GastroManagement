import { computed, inject, Injectable } from "@angular/core";
import { toSignal } from "@angular/core/rxjs-interop";
import { RoleNames } from "@core";
import { RouteAliasService } from "@core/features/routing/services/route-alias-service";
import { IdentityService } from "@core/services/identity.service";
import { MenuItem } from "primeng/api";

@Injectable({
  providedIn: "root",
})
export class MenuService {
  readonly #routeAlias = inject(RouteAliasService);
  readonly #identityService = inject(IdentityService);

  readonly #defaultMenuItems: MenuItem[] = [
    {
      label: "Acasă",
      expanded: true,
      items: [
        { label: "Acasă", icon: "pi pi-fw pi-home", routerLink: this.#routeAlias.getRoute("user-home"), routerLinkActiveOptions: { exact: true } },
        {
          label: "Buletine ECO",
          icon: "pi pi-fw pi-table",
          routerLink: this.#routeAlias.getRoute("buletine-eco"),
        },
        {
          label: "Buletine EDS",
          icon: "pi pi-fw pi-table",
          routerLink: this.#routeAlias.getRoute("buletine-eds"),
        },
        {
          label: "Buletine EDI",
          icon: "pi pi-fw pi-table",
          routerLink: this.#routeAlias.getRoute("buletine-edi"),
        },
      ],
    },
  ];

  readonly #loggedInMenuItems: MenuItem[] = [
    {
      label: "Profil",
      expanded: true,
      items: [
        { label: "Profil", icon: "pi pi-fw pi-user", routerLink: this.#routeAlias.getRoute("profile"), routerLinkActiveOptions: { exact: true } },
        { label: "Dispozitive", icon: "pi pi-fw pi-mobile", routerLink: this.#routeAlias.getRoute("devices") },
        { label: "Schimbă parola", icon: "pi pi-fw pi-lock", routerLink: this.#routeAlias.getRoute("change-password") },
      ],
    },
  ];

  readonly #contentMenuItems: MenuItem[] = [
    {
      label: "Conținut",
      expanded: true,
      items: [{ label: "Administrare", icon: "pi pi-fw pi-cog", routerLink: this.#routeAlias.getRoute("manage-content") }],
    },
  ];

  readonly #adminMenuItems: MenuItem[] = [
    {
      label: "Administrare",
      expanded: true,
      items: [
        { label: "Acasă", icon: "pi pi-fw pi-home", routerLink: this.#routeAlias.getRoute("admin-home"), routerLinkActiveOptions: { exact: true } },
        { label: "Utilizatori", icon: "pi pi-fw pi-users", routerLink: this.#routeAlias.getRoute("admin-users") },
        { label: "Roluri", icon: "pi pi-fw pi-lock", routerLink: this.#routeAlias.getRoute("admin-roles") },
        { label: "Permisiuni", icon: "pi pi-fw pi-shield", routerLink: this.#routeAlias.getRoute("admin-claims") },
      ],
    },
  ];

  readonly #isLoggedIn = toSignal(this.#identityService.watchLoggedIn$(), { initialValue: false });
  readonly #isContentEditorLoggedIn = toSignal(this.#identityService.watchAnyUserRole$([RoleNames.Administrator, RoleNames.ContentEditor]), {
    initialValue: false,
  });
  readonly #isAdminLoggedIn = toSignal(this.#identityService.watchUserRole$(RoleNames.Administrator), { initialValue: false });

  readonly menuItems = computed(() => {
    const items: MenuItem[] = [];

    // Always include default items
    items.push(...this.#defaultMenuItems);

    // Add role-based items
    if (this.#isLoggedIn()) {
      items.push(...this.#loggedInMenuItems);
    }

    if (this.#isContentEditorLoggedIn()) {
      items.push(...this.#contentMenuItems);
    }

    if (this.#isAdminLoggedIn()) {
      items.push(...this.#adminMenuItems);
    }

    return items;
  });
}
