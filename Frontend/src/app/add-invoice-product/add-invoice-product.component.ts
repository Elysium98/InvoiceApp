import { Component, Inject, OnInit, Optional } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Invoice } from '../models/invoice';
import { InvoiceDetails } from '../models/details';
import { InvoiceService } from '../services/invoice.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-add-invoice-product',
  templateUrl: './add-invoice-product.component.html',
  styleUrls: ['./add-invoice-product.component.scss'],
})
export class AddInvoiceProductComponent implements OnInit {
  editMode: boolean = false;
  addInvoiceFormGroup: FormGroup;
  invoiceDetails: InvoiceDetails = new InvoiceDetails();
  matTitle: string = '';
  currentInvoice: Invoice;
  convertedDate: string;
  currentDetails: Partial<InvoiceDetails>;
  constructor(
    @Optional() @Inject(MAT_DIALOG_DATA) private data,
    private fb: FormBuilder,
    private invoiceService: InvoiceService,
    private dialogRef: MatDialogRef<AddInvoiceProductComponent>
  ) {
    this.editMode = this.data.editMode;

    if (this.editMode === true) {
      this.addInvoiceFormGroup = this.fb.group({
        productName: [''],
        quantity: [''],
        value: [''],
        unitPrice: [''],
      });
    }
  }

  async ngOnInit(): Promise<void> {
    if (this.editMode === false) {
      this.matTitle = 'Adăugați un produs';
    } else {
      this.matTitle = 'Editați un produs';
    }

    if (this.editMode === false) {
      this.addInvoiceFormGroup = this.fb.group({
        productName: ['', [Validators.required]],
        quantity: ['', Validators.required],
        unitPrice: ['', Validators.required],
        value: ['', Validators.required],
      });
    } else {
      this.currentDetails = this.data.invoiceDetails;

      this.addInvoiceFormGroup = await this.fb.group({
        unitPrice: [this.currentDetails.unitPrice],
        productName: [this.currentDetails.productName],
        value: [this.currentDetails.value],
        quantity: [this.currentDetails.quantity],
      });
    }
  }

  hasAddError(controlName: string, errorName: string) {
    return this.addInvoiceFormGroup.controls[controlName].hasError(errorName);
  }

  async onSubmit() {
    if (this.editMode === false) {
      this.invoiceDetails.locationId = this.data.invoice.locationId;
      this.invoiceDetails.invoiceId = this.data.invoice.invoiceId;
      this.invoiceDetails.productName =
        this.addInvoiceFormGroup.value.productName;
      this.invoiceDetails.quantity = this.addInvoiceFormGroup.value.quantity;
      this.invoiceDetails.unitPrice = this.addInvoiceFormGroup.value.unitPrice;
      this.invoiceDetails.value = this.addInvoiceFormGroup.value.value;
    } else {
      this.invoiceDetails.productName =
        this.addInvoiceFormGroup.value.productName;
      this.invoiceDetails.quantity = this.addInvoiceFormGroup.value.quantity;
      this.invoiceDetails.unitPrice = this.addInvoiceFormGroup.value.unitPrice;
      this.invoiceDetails.value = this.addInvoiceFormGroup.value.value;
    }

    if (this.editMode === false) {
      let model: Partial<InvoiceDetails> = {
        locationId: this.invoiceDetails.locationId,
        invoiceId: this.invoiceDetails.invoiceId,
        productName: this.invoiceDetails.productName,
        quantity: this.invoiceDetails.quantity,
        unitPrice: this.invoiceDetails.unitPrice,
        value: this.invoiceDetails.value,
      };

      this.invoiceService.addInvoiceProduct$(model).subscribe(
        (data) => {
          this.dialogRef.close('save');
        },
        (error) => console.log('error', error)
      );
    } else {
      let model = {
        quantity: this.invoiceDetails.quantity,
        unitPrice: this.invoiceDetails.unitPrice,
        value: this.invoiceDetails.value,
        productName: this.invoiceDetails.productName,
      };

      this.invoiceService
        .updateInvoiceProduct$(this.currentDetails.detailId, model)
        .subscribe(
          (data) => {
            this.dialogRef.close('save');
          },
          (error) => console.log('error', error)
        );
    }
  }
}
