import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { RegistrationComponent } from "./main/registration.component";

const routes: Routes = [
    { path: 'register', component: RegistrationComponent }
]

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class RegistrationRoutingModule {

}
