import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

import { AuthService } from './services/auth.service';
import { AuthorizationService } from './services/authorization.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent {

  title = 'Zoomnews Admin';
  isAuthenticated: Observable<boolean>;
  isDoneLoading: Observable<boolean>;
  canActivateProtectedRoutes: Observable<boolean>;

  constructor(
    private authService: AuthService,
    private authorizationService: AuthorizationService
  ) {

    this.isAuthenticated = this.authService.isAuthenticated$;
    this.isDoneLoading = this.authService.isDoneLoading$;
    this.canActivateProtectedRoutes = this.authService.canActivateProtectedRoutes$;

    this.authService.runInitialLoginSequence();
  }

  login() { this.authService.login(); }
  logout() { this.authService.logout(); }
  refresh() { this.authService.refresh(); }
  reload() { window.location.reload(); }
  clearStorage() { localStorage.clear(); }

  logoutExternally() {
    window.open(this.authService.logoutUrl);
  }

  get hasValidToken() { return this.authService.hasValidToken(); }
  get accessToken() { return this.authService.accessToken; }
  get identityClaims() { return this.authService.identityClaims; }
  get idToken() { return this.authService.idToken; }
}
