import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StateService } from "./stateService.service";
import { ModelModule } from "./models/model.module";
import { MainModule } from "./main/main.module";
import { NavbarComponent } from "./navbar.component";
import { AuthModule } from './auth/auth.module';
import { DragulaModule } from 'ng2-dragula'



@NgModule({
  declarations: [
    AppComponent, NavbarComponent
  ],
  imports: [
    BrowserModule, AppRoutingModule, ModelModule, MainModule, AuthModule, DragulaModule.forRoot()  
  ],
  providers: [StateService],
  bootstrap: [AppComponent]
})
export class AppModule { }
