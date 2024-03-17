import { InvoiceDetails } from './details';

export class Invoice {
  invoiceId: number = 0;
  locationId: number = 0;
  invoiceNumber: string = '';
  invoiceDate: Date = new Date();
  customerName: string = '';
  invoiceDetails: Partial<InvoiceDetails> = new InvoiceDetails();
  constructor(init?: Partial<Invoice>) {
    Object.assign(this, init);
  }
}
