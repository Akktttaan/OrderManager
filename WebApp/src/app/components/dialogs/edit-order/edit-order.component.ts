import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {FormBuilder} from "@angular/forms";
import {ApiClient, OrderItemViewData, ProviderViewData} from "../../../../api/ApiClient";

@Component({
  selector: 'app-edit-order',
  templateUrl: './edit-order.component.html',
  styleUrls: ['./edit-order.component.sass']
})
export class EditOrderComponent implements OnInit {
  dataForm = this.builder.group({
    id: [''],
    number: [''],
    providerId: [''],
    date: [''],
    orderItems: this.builder.array([])
  })
  providers: ProviderViewData[] = [];

  constructor(
    public dialogRef: MatDialogRef<EditOrderComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private builder: FormBuilder,
    private api: ApiClient
  ) {
    this.api.provider()
      .subscribe(x => this.providers = x)
  }

  ngOnInit(): void {
  }

  onSubmit() {

  }

  onNoClick() {
    this.dialogRef.close();
  }
}
