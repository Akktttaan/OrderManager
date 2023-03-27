import {Component, OnInit} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import {ApiClient, OrderFilterModel, OrderViewData} from "../../../../api/ApiClient";
import {BehaviorSubject, first, merge, Subject} from "rxjs";
import {DataFilterModel} from "./dataFilterModel";
import {MatDialog} from "@angular/material/dialog";
import {EditOrderComponent} from "../../dialogs/edit-order/edit-order.component";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {
  dataForFilters = new DataFilterModel();
  refreshSbj = new BehaviorSubject<boolean>(true);
  displayedColumns = ['id','number', 'date', 'providerId', 'provider', 'actions'];
  // @ts-ignore
  orders: OrderViewData[];
  filterForm = new OrderFilterModel();
  isLoadingSbj = new BehaviorSubject<boolean>(false);

  constructor(
    private builder: FormBuilder,
    private api: ApiClient,
    private dialog: MatDialog
  ) {
    merge(this.refreshSbj)
      .subscribe(() => {
        this.api.orderAll()
          .subscribe(data => {
            this.orders = data
          })

        this.api.filterModel()
          .pipe(first())
          .subscribe(data => {
            this.dataForFilters = new DataFilterModel(data);
          })
      })
  }

  ngOnInit(): void {
  }

  editOrder(element: OrderViewData) {

  }

  deleteOrder(element: OrderViewData) {

  }

  addOrder(){
    this.dialog.open(EditOrderComponent, {
      width: '1000px'
    })
  }

  filterOrder(){
    this.isLoadingSbj.next(true)
    const model = this.filterForm;
    this.api.filteredOrder(model.dateFrom, model.dateTo, model.orderNumbers, model.providerIds, model.orderItemUnits, model.orderItemNames, model.providerNames)
      .subscribe(x => {
        this.orders = x;
        this.isLoadingSbj.next(false)
      })
  }
}

