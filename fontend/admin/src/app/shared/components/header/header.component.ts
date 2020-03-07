import { Component } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { NavService } from '../side-nav/nav.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})

export class HeaderComponent {

  screenType: any;
  displaySearch: boolean = true;
  userProfile: object;

  public userName: string = "Authenticating...";
  firstName: string = "";
  lastName: string = "";

  constructor(private authService: AuthService, private navService: NavService) {
    this.authService.isAuthenticated$.subscribe(authenticated => {
      if (authenticated) {
        this.userProfile = this.authService.identityClaims;
        if (this.userProfile['name']) {
          this.userName = 'Welcome ' + this.userProfile['name'];
        }
      }
    });
  }

  public onMenuButtonClick() {
    setTimeout(() => {
      this.navService.NavClick();
    }, 10);
  }
}
