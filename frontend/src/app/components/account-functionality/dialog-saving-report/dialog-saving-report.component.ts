import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogContent, MatDialogRef } from '@angular/material/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-dialog-saving-report',
  standalone: true,
  imports: [
    CommonModule, 
    MatDialogActions, 
    MatDialogContent, 
    MatButtonModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './dialog-saving-report.component.html',
  styleUrl: './dialog-saving-report.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DialogSavingReportComponent {
  isDone: boolean;

  constructor(
    private dialogRef: MatDialogRef<DialogSavingReportComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.isDone = data.isDone;
    dialogRef.disableClose = true;
  }

  changeValue(newValue: boolean) {
    this.isDone = newValue;
  }

  closeDialog() {
    this.dialogRef.close();
  }
}
