import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { APP_INITIALIZER, NgModule } from '@angular/core';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { OAuthModule, OAuthModuleConfig, AuthConfig, ValidationHandler, OAuthStorage, JwksValidationHandler } from 'angular-oauth2-oidc';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { DomSanitizer } from '@angular/platform-browser';

import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatStepperModule } from '@angular/material/stepper';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatTableModule } from '@angular/material/table';
import { MatRippleModule, MAT_DATE_LOCALE, MAT_DATE_FORMATS } from '@angular/material/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule, MatPaginatorModule, MatSortModule } from '@angular/material';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatExpansionModule } from '@angular/material/expansion';
import { CKEditorModule } from 'ng2-ckeditor';
import { MY_FORMATS } from './shared/app.shared.constants';

import { AuthGuardService } from './services/auth-guard.service';
import { UserProfileService } from './services/userprofile.service';

import { AppComponent } from './app.component';
import { NavigationComponent } from './shared/components/navigation/navigation.component';
import { HeaderComponent } from './shared/components/header/header.component';
import { FooterComponent } from './shared/components/footer/footer.component';
import { SideNavComponent } from './shared/components/side-nav/side-nav.component';
import { MenuListItemComponent } from './shared/components/menu-list-item/menu-list-item.component';
import { NavService } from './shared/components/side-nav/nav.service';
import { AppRoutingModule } from './app-routing.module';
import { HomeComponent } from './components/home/home.component';

import { ConfigurationService } from 'src/app/services/configuration.service';
import { StandardHeaderInterceptor } from './shared/interceptors/standard-header.interceptor';
import { DialogComponent } from './shared/components/dialog/dialog.component';
import { NumberOnlyDirective } from './directives/number-only-directive';
import { CanAccessDirective } from './directives/can-access.directive';
import { ErrorHandlerComponent } from './shared/error-handler/error-handler.component';
import { TextTransformerDirective } from './directives/uppercase.directive';
import { TwoDigitDecimalNumberDirective } from './directives/two-digit-decimal-number.directive';
import { CategoryManagementComponent } from './components/category-management/category-management.component';
import { ArticleManagementComponent } from './components/article-management/article-management.component';
import { CommentManagementComponent } from './components/comment-management/comment-management.component';
import { MediaManagementComponent } from './components/media-management/media-management.component';
import { SettingManagementComponent } from './components/setting-management/setting-management.component';

export function storageFactory(): OAuthStorage {
  return sessionStorage;
}

const appInitializerFn = (configurationService: ConfigurationService) => {
  return () => {
    return configurationService
      .loadRuntimeConfig()
      .then( async clientConfiguration => {
        await configurationService.loadConfig(clientConfiguration.apiUrl)
        }
      );
  };
};

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    HeaderComponent,
    FooterComponent,
    SideNavComponent,
    DialogComponent,
    MenuListItemComponent,
    HomeComponent,
    NumberOnlyDirective,
    CanAccessDirective,
    ErrorHandlerComponent,

    TextTransformerDirective,
    TwoDigitDecimalNumberDirective,
    CategoryManagementComponent,
    ArticleManagementComponent,
    CommentManagementComponent,
    MediaManagementComponent,
    SettingManagementComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CommonModule,
    FormsModule,
    FlexLayoutModule,
    HttpClientModule,
    OAuthModule.forRoot({
      resourceServer: {
        allowedUrls: [],
        sendAccessToken: true
      }
    }),
    AppRoutingModule,
    MatToolbarModule,
    MatIconModule,
    MatListModule,
    MatGridListModule,
    MatButtonToggleModule,
    MatButtonModule,
    MatTooltipModule,
    MatStepperModule,
    MatInputModule,
    MatFormFieldModule,
    MatRadioModule,
    MatSelectModule,
    ReactiveFormsModule,
    MatSlideToggleModule,
    MatCardModule,
    MatDividerModule,
    MatTableModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatRippleModule,
    MatProgressSpinnerModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatDialogModule,
    MatSnackBarModule,
    MatTabsModule,
    MatSidenavModule,
    MatDatepickerModule,
    MatMomentDateModule,
    MatAutocompleteModule,
    MatExpansionModule,
    CKEditorModule
  ],
  providers: [
    ConfigurationService,
    { provide: APP_INITIALIZER, useFactory: appInitializerFn, multi: true, deps: [ConfigurationService] },
    UserProfileService,
    AuthGuardService,
    NavService,
    { provide: MAT_DATE_LOCALE, useValue: 'en-GB' },
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: StandardHeaderInterceptor,
      multi: true,
    },
    { provide: OAuthStorage, useFactory: storageFactory }
  ],
  entryComponents: [
    DialogComponent,
  ],
  bootstrap: [AppComponent]
})

export class AppModule {
  constructor(private readonly matIconRegistry: MatIconRegistry, domSanitizer: DomSanitizer) {
    this.matIconRegistry.addSvgIconSet(domSanitizer.bypassSecurityTrustResourceUrl('../../assets/img/icons/mdi.svg'));
  }
}
