import 'package:flutter/material.dart';
import 'package:studenda_mobile/core/presentation/guest_group_selector.dart';
import 'package:studenda_mobile/feature/auth/presentation/pages/email_auth_widget.dart';
import 'package:studenda_mobile/resources/UI/button_widget.dart';
import 'package:studenda_mobile/resources/colors.dart';

class MainAuthWidget extends StatefulWidget {
  const MainAuthWidget({super.key});

  @override
  State<MainAuthWidget> createState() => _MainAuthWidgetState();
}

class _MainAuthWidgetState extends State<MainAuthWidget> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: mainAuthBackgroundColor,
      body: Container(
        alignment: AlignmentDirectional.center,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            const Text(
              "СТУДЕНДА",
              style: TextStyle(color: mainForegroundColor, fontSize: 40),
            ),
            const SizedBox(
              height: 40,
            ),
            StudendaButton(
              title: "Войти",
              event: () {
                Navigator.of(context).push(
                  MaterialPageRoute<void>(
                    builder: (context) => const EmailAuthWidget(),
                  ),
                );
              },
            ),
            const SizedBox(
              height: 34,
            ),
            StudendaButton(
              title: "Войти как гость",
              event: () {
                Navigator.of(context).push(
                  MaterialPageRoute<void>(
                    builder: (context) => const GuestGroupSelectorWidget(),
                  ),
                );
              },
            ),
          ],
        ),
      ),
    );
  }
}
