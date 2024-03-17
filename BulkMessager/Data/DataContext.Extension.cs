using System;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BulkMessager.Data {

    public partial class DataContext {

        public override int SaveChanges() {
            try {
                Validate();
                return base.SaveChanges();
            } catch (ValidationException ex) {

                var msg = $"{nameof(SaveChanges)} validation exception: {ex?.Message}");
                throw new Exception($"Messaging Service Entity Validation Errors: \n{msg}", ex);

            } catch (DbUpdateException ex) {

                var msg = $"{nameof(SaveChanges)} validation exception: {ex?.Message}");
                throw new Exception($"Messaging Service Entity Validation Errors: \n{msg}", ex);
            }
        }

        public override Task<int> SaveChangesAsync() {

            try {
                Validate();
                return base.SaveChangesAsync();
            } catch (ValidationException ex) {
                var msg = $"{nameof(SaveChanges)} validation exception: {ex?.Message}");
                throw new Exception($"Messaging Service Entity Validation Errors: \n{msg}", ex);
            } catch (DbUpdateException ex) {
                var msg = $"{nameof(SaveChanges)} validation exception: {ex?.Message}");
                throw new Exception($"Messaging Service Entity Validation Errors: \n{msg}", ex);
            }
        }

        protected void Validate() {
            var entities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity);

            foreach (var entity in entities) {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext, validateAllProperties: true);
            }
        }
    }
}
