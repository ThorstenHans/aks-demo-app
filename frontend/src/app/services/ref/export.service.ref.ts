import { ExportService } from '../export.service';
import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class ExportServiceRef extends ExportService {
  constructor(private _httpClient: HttpClient) {
    super();
  }

  public generatePdf(sessionId: string, mail: string): Observable<any> {
    return this._httpClient.post(`api/export`, {
      mail: mail,
      sessionId: sessionId,
    });
  }
}
