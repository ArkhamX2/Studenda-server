import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:studenda_mobile/feature/auth/domain/entities/role_entity.dart';

part 'user_entity.freezed.dart';

@freezed
class UserEntity with _$UserEntity {
  const factory UserEntity(
    {
      required int id,
      required RoleEntity role,
    }
  ) = _UserEntity;
}
