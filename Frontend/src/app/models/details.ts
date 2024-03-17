export class InvoiceDetails {
  invoiceId: number = 0;
  locationId: number = 0;
  detailId: number = 0;
  productName: string = '';
  quantity: number = 0;
  unitPrice: number = 0;
  value: number = 0;

  constructor(init?: Partial<InvoiceDetails>) {
    Object.assign(this, init);
  }
}
