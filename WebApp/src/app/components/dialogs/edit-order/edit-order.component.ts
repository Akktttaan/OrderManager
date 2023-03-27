import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {FormArray, FormBuilder, FormGroup} from "@angular/forms";
import {ApiClient, ProviderViewData} from "../../../../api/ApiClient";
import {ErrorHandlerService} from "../error/error-handler.service";
import {ReplaySubject} from "rxjs";

@Component({
  selector: 'app-edit-order',
  templateUrl: './edit-order.component.html',
  styleUrls: ['./edit-order.component.sass']
})
export class EditOrderComponent implements OnInit {
  errorSbj = new ReplaySubject<string>(1);
  dataForm = this.builder.group({
    id: [''],
    number: [''],
    providerId: [''],
    provider: [''],
    date: [''],
    orderItems: this.builder.array([])
  })
  providers: ProviderViewData[] = [];

  constructor(
    public dialogRef: MatDialogRef<EditOrderComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private builder: FormBuilder,
    private api: ApiClient,
    private error: ErrorHandlerService
  ) {
    this.api.provider()
      .subscribe(x => this.providers = x)
    if (!!data) {
      this.dataForm.patchValue(data);
      this.patchFormArray(data.orderItems, this.orderItems)
    }

    this.error.showError(this.errorSbj);
  }

  ngOnInit(): void {
  }

  get orderItems() {
    return this.dataForm.controls["orderItems"] as FormArray;
  }

  onSubmit() {
    if (this.validateOrder()) {
      return;
    }
    this.dialogRef.close(this.dataForm.getRawValue())
  }

  onNoClick() {
    this.dialogRef.close();
  }

  addOrderItem() {
    const formGroup = this.builder.group({
      id: [null],
      orderId: this.dataForm.controls.id.value,
      name: [''],
      quantity: [''],
      unit: ['']
    });
    this.orderItems.push(formGroup)
  }

  deleteOrderItem(index: number) {
    this.orderItems.removeAt(index);
  }

  getOrderItemFormGroups() {
    return this.orderItems.controls as Array<FormGroup>
  }

  patchFormArray(array: Array<any>, formArray: FormArray<any>) {
    array.forEach(x => {
      formArray.push(this.builder.group(x))
    })
  }

  validateOrder(): boolean {
    for (let orderItem of this.getOrderItemFormGroups()) {
      if (orderItem.controls['name'].value == this.dataForm.controls.number.value) {
        this.errorSbj.next("Номер заказа не может быть равен наименованию строки заказа!")
        return true;
      }
    }
    return false;
  }
}
