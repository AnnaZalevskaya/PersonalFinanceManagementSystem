import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PdfReportService {
  private backendUrl = 'https://localhost:44313/api/operations/pdf-operations-reports';

  constructor(private http: HttpClient) { }

  generateFullReport(accountId: string) {
    const url = `${this.backendUrl}/full-report/${accountId}`;

    return this.http.post(url, { });
  }
}