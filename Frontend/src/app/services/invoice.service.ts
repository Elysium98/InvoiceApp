import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Invoice } from '../models/invoice';
import { InvoiceDetails } from '../models/details';

@Injectable({
  providedIn: 'root',
})
export class InvoiceService {
  readonly baseUrl = 'https://localhost:7231/invoices';

  readonly httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  readonly httpOptionsText = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
    responseType: 'text' as 'json',
  };

  constructor(private httpClient: HttpClient) {}

  getInvoices(): Observable<Invoice[]> {
    return this.httpClient.get<Invoice[]>(this.baseUrl, this.httpOptions);
  }

  addInvoice$(model: Partial<Invoice>): Observable<Invoice> {
    let invoice = {
      locationId: model.locationId,
      invoiceNumber: model.invoiceNumber,
      invoiceDate: model.invoiceDate,
      customerName: model.customerName,
      invoiceDetails: [
        {
          detailsId: '',
          productName: model.invoiceDetails.productName,
          quantity: model.invoiceDetails.quantity,
          unitPrice: model.invoiceDetails.unitPrice,
          value: model.invoiceDetails.value,
        },
      ],
    };
    return this.httpClient.post<Invoice>(
      this.baseUrl,
      invoice,
      this.httpOptions
    );
  }

  updateInvoiceProduct$(
    detailId: number,
    model: Partial<InvoiceDetails>
  ): Observable<InvoiceDetails> {
    let invoice = {
      productName: model.productName,
      quantity: model.quantity,
      value: model.value,
      unitPrice: model.unitPrice,
    };
    return this.httpClient.put<InvoiceDetails>(
      'https://localhost:7231/invoiceDetails/' + detailId,
      invoice,
      this.httpOptionsText
    );
  }
  addInvoiceProduct$(
    model: Partial<InvoiceDetails>
  ): Observable<InvoiceDetails> {
    let invoiceDetails: Partial<InvoiceDetails> = {
      locationId: model.locationId,
      invoiceId: model.invoiceId,
      value: model.value,
      unitPrice: model.unitPrice,
      quantity: model.quantity,
      productName: model.productName,
    };
    return this.httpClient.post<InvoiceDetails>(
      'https://localhost:7231/invoiceDetails',
      invoiceDetails,
      this.httpOptions
    );
  }

  updateInvoice$(
    invoiceId: number,
    locationId: number,
    model: Partial<Invoice>
  ): Observable<Invoice> {
    let invoice = {
      locationId: model.locationId,
      invoiceId: model.invoiceId,
      invoiceNumber: model.invoiceNumber,
      invoiceDate: model.invoiceDate,
      customerName: model.customerName,
    };
    return this.httpClient.put<Invoice>(
      this.baseUrl + '/' + invoiceId + '/' + locationId,
      invoice,
      this.httpOptionsText
    );
  }
}
