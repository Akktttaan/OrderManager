import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {RouterModule, Routes} from "@angular/router";
import {HeaderComponent} from './components/common/header/header.component';
import {HomeComponent} from './components/common/home/home.component';
import {MatIconModule} from "@angular/material/icon";
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatButtonModule} from "@angular/material/button";
import {environment} from "../environments/environment";
import {API_BASE_URL, ApiClient, OrderViewData} from "../api/ApiClient";
import {NotFoundComponent} from './components/common/not-found/not-found.component';
import {MatInputModule} from "@angular/material/input";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {MatNativeDateModule} from "@angular/material/core";
import {MatSelectModule} from "@angular/material/select";
import {MatListModule} from "@angular/material/list";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {HttpClientModule} from "@angular/common/http";
import {MatTableModule} from "@angular/material/table";
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";
import {EditOrderComponent} from './components/dialogs/edit-order/edit-order.component';
import {MatDialogModule} from "@angular/material/dialog";
import {OrderViewComponent} from './components/order-view/order-view.component';
import {ErrorComponent} from "./components/dialogs/error/error.component";
import {ErrorHandlerService} from "./components/dialogs/error/error-handler.service";

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },{
    path: 'view-order/:orderId',
    component: OrderViewComponent
  },{
    path: '**',
    component: NotFoundComponent
  }
]

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    NotFoundComponent,
    EditOrderComponent,
    OrderViewComponent,
    ErrorComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(routes),
    MatIconModule,
    MatToolbarModule,
    MatButtonModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatListModule,
    MatDialogModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatTableModule,
    FormsModule,
    MatProgressSpinnerModule
  ],
  providers: [
    {provide: API_BASE_URL, useValue: environment.apiUrl},
    ApiClient,
    ErrorHandlerService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
