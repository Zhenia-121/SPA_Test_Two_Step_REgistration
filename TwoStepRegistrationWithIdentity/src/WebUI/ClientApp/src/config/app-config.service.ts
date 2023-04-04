import { Inject, Injectable, InjectionToken } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AppConfigService {
  private appConfig: AppConfig = {
    apiUrl: "",
    environment: ""
  };

  constructor (@Inject(APP_CONFIG) applicationConfig: AppConfig) {
    this.appConfig = applicationConfig;
    this.appConfig.apiUrl = `${this.appConfig.apiUrl}/api`;
  }

  get apiUrl():string{
    return this.appConfig.apiUrl;
  }

  get environment():EnvironmentKind{
    switch (this.appConfig.environment) {
      case "DV":
          return EnvironmentKind.Development;
      case "PP":
          return EnvironmentKind.PreProduction;
      case "PR":
          return EnvironmentKind.Production;
      default:
          return EnvironmentKind.None;
    }
  }

  get config() {
    return this.appConfig;
  }
}

export interface AppConfig {
  apiUrl: string;
  environment:string;
}

export let APP_CONFIG = new InjectionToken<AppConfig>('APP_CONFIG')

export enum EnvironmentKind{
  None = 0,
  Development = 1,
  PreProduction = 2,
  Production = 3
}
