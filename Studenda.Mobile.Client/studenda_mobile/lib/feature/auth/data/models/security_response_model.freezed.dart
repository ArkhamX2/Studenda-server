// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// ignore_for_file: type=lint
// ignore_for_file: unused_element, deprecated_member_use, deprecated_member_use_from_same_package, use_function_type_syntax_for_parameters, unnecessary_const, avoid_init_to_null, invalid_override_different_default_values_named, prefer_expression_function_bodies, annotate_overrides, invalid_annotation_target, unnecessary_question_mark

part of 'security_response_model.dart';

// **************************************************************************
// FreezedGenerator
// **************************************************************************

T _$identity<T>(T value) => value;

final _privateConstructorUsedError = UnsupportedError(
    'It seems like you constructed your class using `MyClass._()`. This constructor is only meant to be used by freezed and you are not supposed to need it nor use it.\nPlease check the documentation here for more information: https://github.com/rrousselGit/freezed#custom-getters-and-methods');

SecurityResponseModel _$SecurityResponseModelFromJson(
    Map<String, dynamic> json) {
  return _SecurityResponseModel.fromJson(json);
}

/// @nodoc
mixin _$SecurityResponseModel {
  UserModel get user => throw _privateConstructorUsedError;
  String get token => throw _privateConstructorUsedError;
  String get refreshToken => throw _privateConstructorUsedError;

  Map<String, dynamic> toJson() => throw _privateConstructorUsedError;
  @JsonKey(ignore: true)
  $SecurityResponseModelCopyWith<SecurityResponseModel> get copyWith =>
      throw _privateConstructorUsedError;
}

/// @nodoc
abstract class $SecurityResponseModelCopyWith<$Res> {
  factory $SecurityResponseModelCopyWith(SecurityResponseModel value,
          $Res Function(SecurityResponseModel) then) =
      _$SecurityResponseModelCopyWithImpl<$Res, SecurityResponseModel>;
  @useResult
  $Res call({UserModel user, String token, String refreshToken});

  $UserModelCopyWith<$Res> get user;
}

/// @nodoc
class _$SecurityResponseModelCopyWithImpl<$Res,
        $Val extends SecurityResponseModel>
    implements $SecurityResponseModelCopyWith<$Res> {
  _$SecurityResponseModelCopyWithImpl(this._value, this._then);

  // ignore: unused_field
  final $Val _value;
  // ignore: unused_field
  final $Res Function($Val) _then;

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? user = null,
    Object? token = null,
    Object? refreshToken = null,
  }) {
    return _then(_value.copyWith(
      user: null == user
          ? _value.user
          : user // ignore: cast_nullable_to_non_nullable
              as UserModel,
      token: null == token
          ? _value.token
          : token // ignore: cast_nullable_to_non_nullable
              as String,
      refreshToken: null == refreshToken
          ? _value.refreshToken
          : refreshToken // ignore: cast_nullable_to_non_nullable
              as String,
    ) as $Val);
  }

  @override
  @pragma('vm:prefer-inline')
  $UserModelCopyWith<$Res> get user {
    return $UserModelCopyWith<$Res>(_value.user, (value) {
      return _then(_value.copyWith(user: value) as $Val);
    });
  }
}

/// @nodoc
abstract class _$$SecurityResponseModelImplCopyWith<$Res>
    implements $SecurityResponseModelCopyWith<$Res> {
  factory _$$SecurityResponseModelImplCopyWith(
          _$SecurityResponseModelImpl value,
          $Res Function(_$SecurityResponseModelImpl) then) =
      __$$SecurityResponseModelImplCopyWithImpl<$Res>;
  @override
  @useResult
  $Res call({UserModel user, String token, String refreshToken});

  @override
  $UserModelCopyWith<$Res> get user;
}

/// @nodoc
class __$$SecurityResponseModelImplCopyWithImpl<$Res>
    extends _$SecurityResponseModelCopyWithImpl<$Res,
        _$SecurityResponseModelImpl>
    implements _$$SecurityResponseModelImplCopyWith<$Res> {
  __$$SecurityResponseModelImplCopyWithImpl(_$SecurityResponseModelImpl _value,
      $Res Function(_$SecurityResponseModelImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? user = null,
    Object? token = null,
    Object? refreshToken = null,
  }) {
    return _then(_$SecurityResponseModelImpl(
      user: null == user
          ? _value.user
          : user // ignore: cast_nullable_to_non_nullable
              as UserModel,
      token: null == token
          ? _value.token
          : token // ignore: cast_nullable_to_non_nullable
              as String,
      refreshToken: null == refreshToken
          ? _value.refreshToken
          : refreshToken // ignore: cast_nullable_to_non_nullable
              as String,
    ));
  }
}

/// @nodoc
@JsonSerializable()
class _$SecurityResponseModelImpl implements _SecurityResponseModel {
  const _$SecurityResponseModelImpl(
      {required this.user, required this.token, required this.refreshToken});

  factory _$SecurityResponseModelImpl.fromJson(Map<String, dynamic> json) =>
      _$$SecurityResponseModelImplFromJson(json);

  @override
  final UserModel user;
  @override
  final String token;
  @override
  final String refreshToken;

  @override
  String toString() {
    return 'SecurityResponseModel(user: $user, token: $token, refreshToken: $refreshToken)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$SecurityResponseModelImpl &&
            (identical(other.user, user) || other.user == user) &&
            (identical(other.token, token) || other.token == token) &&
            (identical(other.refreshToken, refreshToken) ||
                other.refreshToken == refreshToken));
  }

  @JsonKey(ignore: true)
  @override
  int get hashCode => Object.hash(runtimeType, user, token, refreshToken);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$SecurityResponseModelImplCopyWith<_$SecurityResponseModelImpl>
      get copyWith => __$$SecurityResponseModelImplCopyWithImpl<
          _$SecurityResponseModelImpl>(this, _$identity);

  @override
  Map<String, dynamic> toJson() {
    return _$$SecurityResponseModelImplToJson(
      this,
    );
  }
}

abstract class _SecurityResponseModel implements SecurityResponseModel {
  const factory _SecurityResponseModel(
      {required final UserModel user,
      required final String token,
      required final String refreshToken}) = _$SecurityResponseModelImpl;

  factory _SecurityResponseModel.fromJson(Map<String, dynamic> json) =
      _$SecurityResponseModelImpl.fromJson;

  @override
  UserModel get user;
  @override
  String get token;
  @override
  String get refreshToken;
  @override
  @JsonKey(ignore: true)
  _$$SecurityResponseModelImplCopyWith<_$SecurityResponseModelImpl>
      get copyWith => throw _privateConstructorUsedError;
}
