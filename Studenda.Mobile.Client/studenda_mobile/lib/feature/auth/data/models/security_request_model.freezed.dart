// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// ignore_for_file: type=lint
// ignore_for_file: unused_element, deprecated_member_use, deprecated_member_use_from_same_package, use_function_type_syntax_for_parameters, unnecessary_const, avoid_init_to_null, invalid_override_different_default_values_named, prefer_expression_function_bodies, annotate_overrides, invalid_annotation_target, unnecessary_question_mark

part of 'security_request_model.dart';

// **************************************************************************
// FreezedGenerator
// **************************************************************************

T _$identity<T>(T value) => value;

final _privateConstructorUsedError = UnsupportedError(
    'It seems like you constructed your class using `MyClass._()`. This constructor is only meant to be used by freezed and you are not supposed to need it nor use it.\nPlease check the documentation here for more information: https://github.com/rrousselGit/freezed#custom-getters-and-methods');

SecurityRequestModel _$SecurityRequestModelFromJson(Map<String, dynamic> json) {
  return _SecurityRequestModel.fromJson(json);
}

/// @nodoc
mixin _$SecurityRequestModel {
  String get email => throw _privateConstructorUsedError;
  String get password => throw _privateConstructorUsedError;
  String get roleName => throw _privateConstructorUsedError;

  Map<String, dynamic> toJson() => throw _privateConstructorUsedError;
  @JsonKey(ignore: true)
  $SecurityRequestModelCopyWith<SecurityRequestModel> get copyWith =>
      throw _privateConstructorUsedError;
}

/// @nodoc
abstract class $SecurityRequestModelCopyWith<$Res> {
  factory $SecurityRequestModelCopyWith(SecurityRequestModel value,
          $Res Function(SecurityRequestModel) then) =
      _$SecurityRequestModelCopyWithImpl<$Res, SecurityRequestModel>;
  @useResult
  $Res call({String email, String password, String roleName});
}

/// @nodoc
class _$SecurityRequestModelCopyWithImpl<$Res,
        $Val extends SecurityRequestModel>
    implements $SecurityRequestModelCopyWith<$Res> {
  _$SecurityRequestModelCopyWithImpl(this._value, this._then);

  // ignore: unused_field
  final $Val _value;
  // ignore: unused_field
  final $Res Function($Val) _then;

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? email = null,
    Object? password = null,
    Object? roleName = null,
  }) {
    return _then(_value.copyWith(
      email: null == email
          ? _value.email
          : email // ignore: cast_nullable_to_non_nullable
              as String,
      password: null == password
          ? _value.password
          : password // ignore: cast_nullable_to_non_nullable
              as String,
      roleName: null == roleName
          ? _value.roleName
          : roleName // ignore: cast_nullable_to_non_nullable
              as String,
    ) as $Val);
  }
}

/// @nodoc
abstract class _$$SecurityRequestModelImplCopyWith<$Res>
    implements $SecurityRequestModelCopyWith<$Res> {
  factory _$$SecurityRequestModelImplCopyWith(_$SecurityRequestModelImpl value,
          $Res Function(_$SecurityRequestModelImpl) then) =
      __$$SecurityRequestModelImplCopyWithImpl<$Res>;
  @override
  @useResult
  $Res call({String email, String password, String roleName});
}

/// @nodoc
class __$$SecurityRequestModelImplCopyWithImpl<$Res>
    extends _$SecurityRequestModelCopyWithImpl<$Res, _$SecurityRequestModelImpl>
    implements _$$SecurityRequestModelImplCopyWith<$Res> {
  __$$SecurityRequestModelImplCopyWithImpl(_$SecurityRequestModelImpl _value,
      $Res Function(_$SecurityRequestModelImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? email = null,
    Object? password = null,
    Object? roleName = null,
  }) {
    return _then(_$SecurityRequestModelImpl(
      email: null == email
          ? _value.email
          : email // ignore: cast_nullable_to_non_nullable
              as String,
      password: null == password
          ? _value.password
          : password // ignore: cast_nullable_to_non_nullable
              as String,
      roleName: null == roleName
          ? _value.roleName
          : roleName // ignore: cast_nullable_to_non_nullable
              as String,
    ));
  }
}

/// @nodoc
@JsonSerializable()
class _$SecurityRequestModelImpl implements _SecurityRequestModel {
  const _$SecurityRequestModelImpl(
      {required this.email, required this.password, required this.roleName});

  factory _$SecurityRequestModelImpl.fromJson(Map<String, dynamic> json) =>
      _$$SecurityRequestModelImplFromJson(json);

  @override
  final String email;
  @override
  final String password;
  @override
  final String roleName;

  @override
  String toString() {
    return 'SecurityRequestModel(email: $email, password: $password, roleName: $roleName)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$SecurityRequestModelImpl &&
            (identical(other.email, email) || other.email == email) &&
            (identical(other.password, password) ||
                other.password == password) &&
            (identical(other.roleName, roleName) ||
                other.roleName == roleName));
  }

  @JsonKey(ignore: true)
  @override
  int get hashCode => Object.hash(runtimeType, email, password, roleName);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$SecurityRequestModelImplCopyWith<_$SecurityRequestModelImpl>
      get copyWith =>
          __$$SecurityRequestModelImplCopyWithImpl<_$SecurityRequestModelImpl>(
              this, _$identity);

  @override
  Map<String, dynamic> toJson() {
    return _$$SecurityRequestModelImplToJson(
      this,
    );
  }
}

abstract class _SecurityRequestModel implements SecurityRequestModel {
  const factory _SecurityRequestModel(
      {required final String email,
      required final String password,
      required final String roleName}) = _$SecurityRequestModelImpl;

  factory _SecurityRequestModel.fromJson(Map<String, dynamic> json) =
      _$SecurityRequestModelImpl.fromJson;

  @override
  String get email;
  @override
  String get password;
  @override
  String get roleName;
  @override
  @JsonKey(ignore: true)
  _$$SecurityRequestModelImplCopyWith<_$SecurityRequestModelImpl>
      get copyWith => throw _privateConstructorUsedError;
}
