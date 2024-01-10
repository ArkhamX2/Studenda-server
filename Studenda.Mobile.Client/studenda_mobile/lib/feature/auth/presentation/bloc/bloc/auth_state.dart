part of 'auth_bloc.dart';

@freezed
class AuthState with _$AuthState {
  const factory AuthState.initial() = _Initial;
  const factory AuthState.authSuccess(UserEntity user) = _AuthSuccess;
  const factory AuthState.authLoading() = _AuthLoading;
  const factory AuthState.authFail([@Default("Unknown error") String message]) = _AuthFail;
  
}

