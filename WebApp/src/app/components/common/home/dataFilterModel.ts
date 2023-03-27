import {IOrderFilterModel} from "../../../../api/ApiClient";

export class DataFilterModel {
  dateFrom?: Date | undefined;
  dateTo?: Date | undefined;
  orderNumbers?: string[] | undefined;
  providerIds?: string[] | undefined;
  orderItemUnits?: string[] | undefined;
  orderItemNames?: string[] | undefined;
  providerNames?: string[] | undefined;

  constructor(data?: IOrderFilterModel) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property))
          (<any>this)[property] = (<any>data)[property];
      }
    }
  }

}
