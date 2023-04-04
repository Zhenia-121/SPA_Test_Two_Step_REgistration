export class UserRegisterRequest {
  email?: string;
  password?: string;
  confirmedPassword?: string;
  isAgreementAccepted?: boolean;
  provinceId?: number;
}
