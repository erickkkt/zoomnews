import { NgModule, ErrorHandler } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthService } from './services/auth.service';
import { AuthGuardService } from './services/auth-guard.service';
import { UserProfileService } from './services/userprofile.service';
import { HomeComponent } from './components/home/home.component';
import { ErrorHandlerComponent } from './shared/error-handler/error-handler.component';
import { CategoryManagementComponent } from './components/category-management/category-management.component';
import { ArticleManagementComponent } from './components/article-management/article-management.component';
import { MediaManagementComponent } from './components/media-management/media-management.component';
import { CommentManagementComponent } from './components/comment-management/comment-management.component';
import { SettingManagementComponent } from './components/setting-management/setting-management.component';


const routes: Routes = [
  { path: '', redirectTo: '/app', pathMatch: 'full' },
  {
    path: 'app',
    children: [
      { path: '', component: HomeComponent, canActivate: [AuthGuardService], pathMatch: 'full' },
      
      // {
      //   path: 'settings',
      //   children: [
      //     { path: 'system-setting', component: SystemSettingComponent },
      //     { path: 'broker-setting', component: BrokerSettingComponent },
      //   ]
      // },
      { path: 'categories', component: CategoryManagementComponent },      
      { path: 'articles', component: ArticleManagementComponent },
      { path: 'medias', component: MediaManagementComponent },
      { path: 'comments', component: CommentManagementComponent },    
      { path: 'settings', component: SettingManagementComponent },
      { path: 'error/:errorCode/:exceptionId/:message', component: ErrorHandlerComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [
    AuthService,
    UserProfileService,
    AuthGuardService
  ]
})
export class AppRoutingModule { }
