import { Component, ViewChild, ElementRef } from '@angular/core';
import { NavService } from './nav.service';
import { NavItem } from './nav-item.model';
import { AuthService } from 'src/app/services/auth.service';
import { HttpBaseService } from '../../services/http-base.service';
import { Observable } from 'rxjs';
import { ApiEndPoints } from '../../config/ApiEndPoints';

@Component({
    selector: 'app-side-nav',
    templateUrl: './side-nav.component.html',
    styleUrls: ['./side-nav.component.scss']
})

export class SideNavComponent {
    roles: string[] = [];
    navItems$: Observable<NavItem[]>;
    constructor(private navService: NavService, private authService: AuthService, private httpService: HttpBaseService,
        private api: ApiEndPoints) {
        this.authService.isAuthenticated$.subscribe(async authenticated => {
            if (authenticated) {
                this.roles = this.authService.getUserRoles;
                console.log(this.roles);
                console.log(authenticated);
                this.navItems$ = this.httpService.getData<NavItem[]>(this.api.getAuthorizedNavItems());
                let items = await this.httpService.getDataAsync<NavItem[]>(this.api.getAuthorizedNavItems());
                console.log(items);
            }
        });
    }
    public get userRoles() {
        return this.roles;
    }
    @ViewChild('appDrawer') appDrawer: ElementRef;

    ngAfterViewInit(): void {
        this.navService.appDrawer = this.appDrawer;
    }
}
