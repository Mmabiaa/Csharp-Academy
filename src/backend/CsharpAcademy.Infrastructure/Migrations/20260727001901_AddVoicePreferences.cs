using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CsharpAcademy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVoicePreferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "VoiceFeedbackEnabled",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VoiceRecognitionEnabled",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoiceFeedbackEnabled",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VoiceRecognitionEnabled",
                table: "AspNetUsers");
        }
    }
}
