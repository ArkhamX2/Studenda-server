import 'package:email_validator/email_validator.dart';
import 'package:flutter/material.dart';
import 'package:studenda_mobile/feature/auth/presentation/pages/verification_auth_widget.dart';
import 'package:studenda_mobile/resources/UI/button_widget.dart';
import 'package:studenda_mobile/resources/colors.dart';

class EmailAuthWidget extends StatefulWidget {
  const EmailAuthWidget({super.key});

  @override
  State<EmailAuthWidget> createState() => _EmailAuthWidgetState();
}

class _EmailAuthWidgetState extends State<EmailAuthWidget> {
  final _emailTextController = TextEditingController();
  final formKey = GlobalKey<FormState>();

  @override
  void dispose() {
    _emailTextController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: mainBackgroundColor,
      appBar: AppBar(
        leading: IconButton(
          icon: const Icon(Icons.chevron_left_sharp,
          color: Colors.white,),
          onPressed: () => {Navigator.of(context).pop()},
        ),
        titleSpacing: 0,
        centerTitle: true,
        title: const Text(
          'Вход',
          style: TextStyle(color: Colors.white, fontSize: 25),
        ),
      ),
      body: Container(
        alignment: AlignmentDirectional.center,
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 17),
          child: Form(
            key: formKey,
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.stretch,
              children: [
                const Text(
                  "Введите свой email:",
                  style: TextStyle(
                    color: mainForegroundColor,
                    fontSize: 20,
                  ),
                ),
                const SizedBox(
                  height: 23,
                ),
                _EmailFieldWidget(controller: _emailTextController),
                const SizedBox(
                  height: 23,
                ),
                Center(
                  child: StudendaButton(
                    title: "Подтвердить",
                    event: () {
                      //TODO: Get the code
                      final form = formKey.currentState!;
                      if (form.validate()) {
                        final email = _emailTextController.text;
                        Navigator.of(context).push(
                          MaterialPageRoute<void>(
                            builder: (context) =>
                                VerificationAuthWidget(email: email),
                          ),
                        );
                      }
                    },
                  ),
                ),
                const SizedBox(
                  height: 34,
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}

class _EmailFieldWidget extends StatefulWidget {
  final TextEditingController controller;

  const _EmailFieldWidget({
    required this.controller,
  });

  @override
  State<_EmailFieldWidget> createState() => _EmailFieldWidgetState();
}

class _EmailFieldWidgetState extends State<_EmailFieldWidget> {
  @override
  void initState() {
    super.initState();

    widget.controller.addListener(onListen);
  }

  @override
  void dispose() {
    widget.controller.removeListener(onListen);
    super.dispose();
  }

  void onListen() {
    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      controller: widget.controller,
      decoration: InputDecoration(
        border: OutlineInputBorder(borderRadius: BorderRadius.circular(5)),
      ),
      keyboardType: TextInputType.emailAddress,
      autofillHints: const [AutofillHints.email],
      validator: (email) => email != null && !EmailValidator.validate(email)
          ? "Введён некорректный email"
          : null,
    );
  }
}
