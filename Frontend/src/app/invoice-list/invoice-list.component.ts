import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { AddInvoiceComponent } from '../add-invoice/add-invoice.component';
import { MatDialog } from '@angular/material/dialog';
import { InvoiceService } from '../services/invoice.service';
import { Invoice } from '../models/invoice';
import { AddInvoiceProductComponent } from '../add-invoice-product/add-invoice-product.component';
import { InvoiceDetails } from '../models/details';

@Component({
  selector: 'app-invoice-list',
  templateUrl: './invoice-list.component.html',
  styleUrls: ['./invoice-list.component.scss'],
})
export class InvoiceListComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  invoices: MatTableDataSource<Invoice> = new MatTableDataSource();
  invoices_header: string[] = ['invoices_header'];

  displayedInvoicesColumns: string[] = [
    'customerName',
    'invoiceDate',
    'invoiceNumber',
    'invoiceDetails',
    'actions',
  ];
  constructor(
    private invoiceService: InvoiceService,
    public dialog: MatDialog
  ) {}

  ngOnInit() {
    this.getInvoices();
  }

  getInvoices() {
    this.invoiceService.getInvoices().subscribe((result) => {
      this.invoices = new MatTableDataSource(result);
      this.invoices.paginator = this.paginator;
    });
  }

  openInvoiceDialogToAdd() {
    this.dialog
      .open(AddInvoiceComponent, {
        width: '1000px',
        height: 'auto',
      })
      .afterClosed()
      .subscribe((val) => {
        if (val === 'save') {
          this.getInvoices();
        }
      });
  }

  openInvoiceProductDialogToAdd(invoice: Invoice) {
    this.dialog
      .open(AddInvoiceProductComponent, {
        width: '1000px',
        height: 'auto',
        data: { invoice: invoice, editMode: false },
      })
      .afterClosed()
      .subscribe((val) => {
        if (val === 'save') {
          this.getInvoices();
        }
      });
  }

  openInvoiceProductDialogToEdit(invoiceDetails: InvoiceDetails) {
    this.dialog
      .open(AddInvoiceProductComponent, {
        width: '1000px',
        height: 'auto',
        data: { invoiceDetails: invoiceDetails, editMode: true },
      })
      .afterClosed()
      .subscribe((val) => {
        if (val === 'save') {
          this.getInvoices();
        }
      });
  }

  openInvoiceDialogToEdit(invoice: Invoice) {
    this.dialog
      .open(AddInvoiceComponent, {
        width: '1000px',
        height: 'auto',
        data: invoice,
      })
      .afterClosed()
      .subscribe((val) => {
        if (val === 'save') {
          this.getInvoices();
        }
      });
  }
}
