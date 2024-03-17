import { Component, Inject, OnInit, Optional } from '@angular/core';
import { Invoice } from '../models/invoice';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { InvoiceService } from '../services/invoice.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { InvoiceDetails } from '../models/details';

@Component({
  selector: 'app-add-invoice',
  templateUrl: './add-invoice.component.html',
  styleUrls: ['./add-invoice.component.scss'],
})
export class AddInvoiceComponent implements OnInit {
  editMode: boolean = false;
  addInvoiceFormGroup: FormGroup;
  invoice: Invoice = new Invoice();
  matTitle: string = '';
  currentInvoice: Invoice;
  convertedDate: string;
  currentDetails: Partial<InvoiceDetails>;
  constructor(
    @Optional() @Inject(MAT_DIALOG_DATA) private data: Invoice,
    private fb: FormBuilder,
    private invoiceService: InvoiceService,
    private dialogRef: MatDialogRef<AddInvoiceComponent>,
    private datePipe: DatePipe
  ) {
    this.editMode = this.data != null ? true : false;

    if (this.editMode === true) {
      this.addInvoiceFormGroup = this.fb.group({
        locationId: [''],
        invoiceNumber: [''],
        invoiceDate: [''],
        customerName: [''],
      });
    }
  }

  async ngOnInit(): Promise<void> {
    if (this.editMode === false) {
      this.matTitle = 'Adăugați o factură';
    } else {
      this.matTitle = 'Editați o factură';
    }

    if (this.editMode === false) {
      this.addInvoiceFormGroup = this.fb.group({
        invoiceNumber: ['', [Validators.required, Validators.maxLength(100)]],
        invoiceDate: ['', Validators.required],
        value: ['', Validators.required],
        quantity: ['', Validators.required],
        customerName: ['', Validators.required],
        productName: ['', Validators.required],
        unitPrice: ['', Validators.required],
      });
    } else {
      this.currentInvoice = this.data;
      console.log(this.currentInvoice);
      this.convertedDate = this.datePipe.transform(
        this.currentInvoice.invoiceDate,
        'yyyy-MM-dd'
      );

      this.addInvoiceFormGroup = await this.fb.group({
        invoiceNumber: [
          this.currentInvoice.invoiceNumber,
          Validators.maxLength(100),
        ],
        invoiceDate: [this.convertedDate],
        customerName: [this.currentInvoice.customerName],
      });
    }
  }

  hasAddError(controlName: string, errorName: string) {
    return this.addInvoiceFormGroup.controls[controlName].hasError(errorName);
  }

  async onSubmit() {
    if (this.editMode === false) {
      this.invoice.customerName = this.addInvoiceFormGroup.value.customerName;
      this.invoice.invoiceDate = this.addInvoiceFormGroup.value.invoiceDate;
      this.invoice.invoiceId = this.addInvoiceFormGroup.value.invoiceId;
      this.invoice.invoiceNumber = this.addInvoiceFormGroup.value.invoiceNumber;
      this.invoice.locationId = this.addInvoiceFormGroup.value.locationId;
      this.invoice.invoiceDetails.productName =
        this.addInvoiceFormGroup.value.productName;
      this.invoice.invoiceDetails.quantity =
        this.addInvoiceFormGroup.value.quantity;
      this.invoice.invoiceDetails.unitPrice =
        this.addInvoiceFormGroup.value.unitPrice;
      this.invoice.invoiceDetails.value = this.addInvoiceFormGroup.value.value;
    } else {
      this.invoice.customerName = this.addInvoiceFormGroup.value.customerName;
      this.invoice.invoiceDate = this.addInvoiceFormGroup.value.invoiceDate;
      this.invoice.invoiceNumber = this.addInvoiceFormGroup.value.invoiceNumber;
    }

    if (this.editMode === false) {
      let model: Partial<Invoice> = {
        invoiceNumber: this.invoice.invoiceNumber,
        invoiceDate: this.invoice.invoiceDate,
        customerName: this.invoice.customerName,
        invoiceDetails: {
          productName: this.invoice.invoiceDetails.productName,
          quantity: this.invoice.invoiceDetails.quantity,
          unitPrice: this.invoice.invoiceDetails.unitPrice,
          value: this.invoice.invoiceDetails.value,
        },
      };

      console.log(model);
      this.invoiceService.addInvoice$(model).subscribe(
        (data) => {
          this.dialogRef.close('save');
        },
        (error) => console.log('error', error)
      );
    } else {
      let model = {
        invoiceNumber: this.invoice.invoiceNumber,
        invoiceDate: this.invoice.invoiceDate,
        customerName: this.invoice.customerName,
      };

      this.invoiceService
        .updateInvoice$(
          this.currentInvoice.invoiceId,
          this.currentInvoice.locationId,
          model
        )
        .subscribe(
          (data) => {
            this.dialogRef.close('save');
          },
          (error) => console.log('error', error)
        );
    }
  }
}
