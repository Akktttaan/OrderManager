import {Component, OnInit} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import {ApiClient, ApiException, OrderFilterModel, OrderViewData} from "../../../../api/ApiClient";
import {BehaviorSubject, filter, first, merge, Subject} from "rxjs";
import {DataFilterModel} from "./dataFilterModel";
import {MatDialog} from "@angular/material/dialog";
import {EditOrderComponent} from "../../dialogs/edit-order/edit-order.component";
import {ErrorHandlerService} from "../../dialogs/error/error-handler.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {
  errorSbj = new Subject<string>();
  dataForFilters = new DataFilterModel();
  refreshSbj = new BehaviorSubject<boolean>(true);
  displayedColumns = ['id','number', 'date', 'providerId', 'provider'];
  // @ts-ignore
  orders: OrderViewData[];
  filterForm = new OrderFilterModel();
  isLoadingSbj = new BehaviorSubject<boolean>(false);

  constructor(
    private builder: FormBuilder,
    private api: ApiClient,
    private dialog: MatDialog,
    private error: ErrorHandlerService,
    private router: Router
  ) {
    merge(this.refreshSbj)
      .subscribe(() => {
        this.api.orderAll()
          .subscribe(data => {
            this.orders = data;
            this.filterForm = new OrderFilterModel();
          })

        this.api.filterModel()
          .pipe(first())
          .subscribe(data => {
            this.dataForFilters = new DataFilterModel(data);
          })
      })

    this.error.showError(this.errorSbj);
  }

  ngOnInit(): void {
  }

  addOrder(){
    this.dialog.open(EditOrderComponent, {
      width: '1000px'
    })
      .afterClosed()
      .pipe(
        first(),
        filter(x => !!x)
      )
      .subscribe(order => {
        this.api.orderPOST(order)
          .pipe(first())
          .subscribe(
            () => this.refreshSbj.next(true),
            (error: ApiException) => this.errorSbj.next(error.response))
      })
  }

  filterOrder(){
    this.isLoadingSbj.next(true)
    const model = this.filterForm;
    this.api.filteredOrder(
      model.dateFrom,
      model.dateTo,
      model.orderNumbers,
      model.providerIds,
      model.orderItemUnits,
      model.orderItemNames,
      model.providerNames)
      .pipe(first())
      .subscribe(x => {
        this.orders = x;
        this.isLoadingSbj.next(false)
      })
  }

  async openOrder(event: any, element: OrderViewData) {
    if (event.target.tagName.toLowerCase() !== 'td') {
      return;
    }
    console.log(element);
    await this.router.navigate(['view-order', element.id]);
  }
}

