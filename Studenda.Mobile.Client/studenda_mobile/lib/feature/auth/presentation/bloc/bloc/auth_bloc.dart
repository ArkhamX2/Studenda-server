import 'package:bloc/bloc.dart';
import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:studenda_mobile/feature/auth/data/models/security_request_model.dart';
import 'package:studenda_mobile/feature/auth/domain/entities/role_entity.dart';
import 'package:studenda_mobile/feature/auth/domain/entities/user_entity.dart';
import 'package:studenda_mobile/feature/auth/domain/usecases/auth.dart';

part 'auth_event.dart';
part 'auth_state.dart';
part 'auth_bloc.freezed.dart';

class AuthBloc extends Bloc<AuthEvent, AuthState> {
  final Auth authUseCase;

  AuthBloc({required this.authUseCase}) : super(const _Initial()) {
    on<_Auth>((event, emit) async {
      emit(const AuthState.authLoading());
      final user = await authUseCase(event.authRequest);
      user.fold(
        (l) => emit(
          AuthState.authFail(l.message),
        ),
        (r) => emit(
          AuthState.authSuccess(
            UserEntity(
              id: r.user.id,
              role: RoleEntity(
                id: r.user.role.id,
                name: r.user.role.name,
              ),
            ),
          ),
        ),
      );
    });
  }
}
