import { Component, OnInit } from '@angular/core';
import {ApiClient, ApiException, OrderItemViewData, OrderViewData, ProviderViewData} from "../../../api/ApiClient";
import {FormBuilder} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {BehaviorSubject, filter, first, merge, Subject} from "rxjs";
import {MatDialog} from "@angular/material/dialog";
import {EditOrderComponent} from "../dialogs/edit-order/edit-order.component";
import {ErrorHandlerService} from "../dialogs/error/error-handler.service";

@Component({
  selector: 'app-order-view',
  templateUrl: './order-view.component.html',
  styleUrls: ['./order-view.component.sass']
})
export class OrderViewComponent implements OnInit {
  errorSbj = new Subject<string>();
  // @ts-ignore
  order: OrderViewData;
  // @ts-ignore
  providerName: string;
  orderId: number;
  refreshSbj = new BehaviorSubject<boolean>(true);
  // @ts-ignore
  orderItems: OrderItemViewData[];
  displayedColumns = ['id', 'name', 'quantity', 'unit'];
  constructor(
    private api: ApiClient,
    private builder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private error: ErrorHandlerService
  ) {
    const {orderId} = this.route.snapshot.params;
    this.orderId = orderId;

    merge(this.refreshSbj)
      .subscribe(() => {
        this.api.orderById(this.orderId)
          .pipe(first())
          .subscribe(data => {
            console.log(data);
            this.order = new OrderViewData(data);
            // @ts-ignore
            this.orderItems = data.orderItems
            // @ts-ignore
            this.providerName = data.provider.name
          })
      })

    this.error.showError(this.errorSbj)
  }

  ngOnInit(): void {
  }

  deleteOrder() {
    this.api.orderDELETE(this.order)
      .subscribe(() => {
        this.router.navigate([''])
      })
  }

  editOrder() {
    this.dialog.open(EditOrderComponent, {
      width: '1000px',
      data: this.order
    })
      .afterClosed()
      .pipe(first(), filter(x => !!x))
      .subscribe(data => {
        this.api.orderPUT(data)
          .pipe(first())
          .subscribe(
          () => this.refreshSbj.next(true),
          (error: ApiException) => this.errorSbj.next(error.response))
      })
  }
}
