import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCardModule } from '@angular/material/card';
import { RegistrationModule } from './registration/registration.module';

@NgModule({
  declarations: [
    AppComponent
   ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    RegistrationModule,
    RouterModule.forRoot([
      {
        path: '',
        redirectTo: '/register',
        pathMatch: 'full'
      },
    ])
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
