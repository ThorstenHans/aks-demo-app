import { ExportService } from '../export.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';


@Injectable()
export class ExportServiceRef extends ExportService {

  constructor(private readonly _http: HttpClient) {
    super();
  }

  public exportSession(sessionId: string): Observable<any> {
    return this._http.post(`/api/export/${sessionId}`, null);
  }

}
